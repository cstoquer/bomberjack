using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Pixelbox;
using Bomberman.Tiles;

public class Stage : MonoBehaviour {

	private const int TILE_SIZE = 16;

	// tile sprite codes
	private const int BRICK = 1;
	private const int SPAWNING_POINT = 3;

	public GameObject[] tilesheet;
	public GameObject[] powerups;

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
		instance = this;

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
				Tile tile = tileObj.GetComponent<Tile>();
				tiles[i, j] = tile;
				tile.Init(item.x, item.y);
				tileObj.transform.SetParent(transform);

				if (item.flagA > 0 && item.flagA < powerups.Length) {
					((Destructible)tile).contentPrefab = powerups[item.flagA];
				}
			}
		}

		Camera.main.GetComponent<CameraMan>().InitPosition(this);
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	private Map RandomiseMap(Map original, float empty = 0.25f) {

		Map map = original.Clone();

		// find all bricks tiles in the map
		MapItem[] bricks = map.Find(BRICK);

		int i;
		int len = 0;

		int EMPTY = (int)((float)bricks.Length * empty);
		
		// shuffle bricks array
		for (i = bricks.Length - 1; i >= 0; i--) {
			int r = (int)UnityEngine.Random.Range(0, i);
			MapItem temp = bricks[r];
			bricks[r] = bricks[i];
			bricks[i] = temp;
		}

		// first n bricks become empty tiles
		i = len; len += EMPTY;
		for (; i < len; i++) {
			MapItem item = bricks[i];
			map.items[item.x, item.y] = null;
		}

		// next m bricks get bomb powerups inside
		i = len; len += 10;
		for (; i < len; i++) {
			MapItem item = bricks[i];
			map.items[item.x, item.y].flagA = 1;
		}

		// TODO

		return map;
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	public Tile AddTile(int i, int j, GameObject tilePrefab) {
		if (tilePrefab == null) return emptyTile;
		if (i < 0 || j < 0 || i >= width || j >= height) return emptyTile;
		GameObject tileObj = (GameObject)Instantiate(tilePrefab, new Vector3(i * TILE_SIZE, -j * TILE_SIZE, 0f), Quaternion.identity);
		Tile tile = tiles[i, j] = tileObj.GetComponent<Tile>();
		tile.Init(i, j);
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
	public void RemoveTile(int i, int j, Tile tile) {
		if (i < 0 || j < 0 || i >= width || j >= height) return;
		if (tiles[i, j] != tile) return;
		tiles[i, j] = emptyTile;
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	public Tile GetTile(int i, int j) {
		if (i < 0 || j < 0 || i >= width || j >= height) return emptyTile;
		return tiles[i, j];
	}
}
