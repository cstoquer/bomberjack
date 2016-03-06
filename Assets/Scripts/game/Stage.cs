using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Pixelbox;
using Bomberman;
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

				if (item == null || item.sprite >= tilesheet.Length || tilesheet[item.sprite] == null) {
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
	private Map RandomiseMap(Map original) {

		Map map = original.Clone();

		// find all bricks tiles in the map
		MapItem[] bricks = map.Find(BRICK);

		int i;
		
		// shuffle bricks array
		for (i = bricks.Length - 1; i >= 0; i--) {
			int r = (int)UnityEngine.Random.Range(0, i);
			MapItem temp = bricks[r];
			bricks[r] = bricks[i];
			bricks[i] = temp;
		}

		// first bricks become empty tiles
		int len = (int)((float)bricks.Length * Game.instance.EMPTY_PERCENT);
		for (i = 0; i < len; i++) {
			MapItem item = bricks[i];
			map.items[item.x, item.y] = null;
		}

		// next bricks get powerups inside
		int[] powerups = {
			Game.instance.BOMB,
			Game.instance.FLAME,
			Game.instance.SPEED,
			Game.instance.PUNCH,
			Game.instance.KICK,
			Game.instance.SUPER_FLAME
		};

		for (int p = 0; p < powerups.Length; p++) {
			for (len += powerups[p]; i < len; i++) {
				MapItem item = bricks[i];
				map.items[item.x, item.y].flagA = p + 1;
			}
		}

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
	public void RemoveTile(Tile tile) {
		int i = tile.i;
		int j = tile.j;
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
