using UnityEngine;
using System.Collections;
using Pixelbox;
using Bomberman.Tiles;

public class Stage : MonoBehaviour {

	private const int TILE_SIZE = 16;

	// tile sprite codes
	private const int BRICK = 1;
	private const int SPAWNING_POINT = 3;

	public GameObject[] tilesheet;

	[HideInInspector] public int width;
	[HideInInspector] public int height;
	[HideInInspector] public int[,] grid;
	[HideInInspector] public Tile[,] tiles;
	[HideInInspector] public MapItem[] spawnpoints;


	public static Stage instance;

	// FIXME MonoBehaviour cannot be created with 'new' keyword. should use AddComponent()
	private static Tile emptyTile = new Tile(0, 0, true);

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	public void Init(string stage) {
		Map map = Maps.GetMap(stage);
		map = RandomiseMap(map);

		spawnpoints = map.Find(SPAWNING_POINT);

		width = map.width;
		height = map.height;
		grid   = new int[width, height];
		tiles  = new Tile[width, height];

		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				MapItem item = map.Get(i, j);
				if (item == null || item.sprite == SPAWNING_POINT) {
					tiles[i, j] = emptyTile;
					continue;
				}
				GameObject tileObj = (GameObject)Instantiate(tilesheet[item.sprite], new Vector3(i * TILE_SIZE, -j * TILE_SIZE, 0f), Quaternion.identity);
				tiles[i, j] = tileObj.GetComponent<Tile>();
				tiles[i, j].Init(item, this);
				tileObj.transform.SetParent(transform);
			}
		}

		instance = this;
		Camera.main.GetComponent<CameraMan>().InitPosition(this);
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	private Map RandomiseMap(Map original, float empty = 0.25f) {

		Map map = original.Clone();

		// find all bricks tiles in the map
		MapItem[] bricks = map.Find(BRICK);

		int EMPTY = (int)((float)bricks.Length * empty);

		// shuffle bricks array
		for (int i = bricks.Length - 1; i >= 0; i--) {
			int r = (int)Random.Range(0, i);
			MapItem temp = bricks[r];
			bricks[r] = bricks[i];
			bricks[i] = temp;
		}

		// first n bricks become empty tiles
		for (int i = 0; i < EMPTY; i++) {
			MapItem item = bricks[i];
			map.items[item.x, item.y] = null;
		}

		// next m bricks get powerups inside

		// TODO

		return map;
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	// This function is primaly used by players to add bombs on the stage
	public Tile AddTile(int i, int j, int item) {
		if (i < 0 || j < 0 || i >= width || j >= height) return emptyTile;
		GameObject tileObj = (GameObject)Instantiate(tilesheet[item], new Vector3(i * TILE_SIZE, -j * TILE_SIZE, 0f), Quaternion.identity);
		tiles[i, j] = tileObj.GetComponent<Tile>();
		//tiles[i, j].Init(item, this); // TODO
		tileObj.transform.SetParent(transform);
		return tiles[i, j];
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	public void SetTile(int i, int j, Tile tile = null) {
		if (i < 0 || j < 0 || i >= width || j >= height) return;
		if (tile == null) tile = emptyTile; 
		tiles[i, j] = tile;
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	public Tile GetTile(int i, int j) {
		if (i < 0 || j < 0 || i >= width || j >= height) return emptyTile;
		return tiles[i, j];
	}
}
