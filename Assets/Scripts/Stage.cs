using UnityEngine;
using System.Collections;
using Pixelbox;

public class Stage : MonoBehaviour {
	public Sprite[] spritesheet;

	[HideInInspector] public int width;
	[HideInInspector] public int height;
	[HideInInspector] public int[,] grid;
	[HideInInspector] public Tile[,] tiles;

	void Start() {
		CreateStage("classic");
	}

	public void CreateStage(string stage) {
		Map map = Maps.GetMap(stage);

		width  = map.width;
		height = map.height;
		grid   = new int[width, height];
		tiles  = new Tile[width, height];

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				MapItem item = map.Get(x, y);
				if (item == null) continue;

				Tile tile = new Tile(spritesheet[item.sprite], x, y);
				tiles[x, y] = tile;
				tile.gameObject.transform.SetParent(transform);
			}
		}
	}

	public Tile GetTile(int x, int y) {
		if (x < 0 || y < 0 || x >= width || y >= height) return null;
		return tiles[x, y];
	}
}
