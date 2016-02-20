using UnityEngine;
using System.Collections;

public class Tile {
	private const int TILE_SIZE = 16;

	public GameObject gameObject;
	public int x;
	public int y;

	public Tile(Sprite sprite, int x, int y) {
		this.x = x;
		this.y = y;

		gameObject = new GameObject("tile[" + x + "," + y + "]");
		SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
		spriteRenderer.sprite = sprite;
		spriteRenderer.sortingOrder = y;
		gameObject.transform.position = new Vector3(x * TILE_SIZE, -y * TILE_SIZE, 0f);
	}
}
