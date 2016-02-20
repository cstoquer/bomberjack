using UnityEngine;
using System.Collections;
using Pixelbox;

public class Stage : MonoBehaviour {
	public Sprite[] spritesheet;

	private const int TILE_SIZE = 16;

	[HideInInspector] public int width;
	[HideInInspector] public int height;
	[HideInInspector] public int[,] grid;
	[HideInInspector] public GameObject[,] tiles;

	void Start() {
		Create("classic");
	}

	public void Create(string stage) {
		Map map = Maps.GetMap(stage);

		width  = map.width;
		height = map.height;
		grid   = new int[width, height];
		tiles  = new GameObject[width, height];


		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				MapItem item = map.Get(x, y);
				if (item == null) continue;

				GameObject gameObject = new GameObject("tile[" + x + "," + y + "]");
				SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
				spriteRenderer.sprite = spritesheet[item.sprite];
				spriteRenderer.sortingOrder = y;
				gameObject.transform.SetParent(transform);
				tiles[x, y] = gameObject;
				gameObject.transform.position = new Vector3(x * TILE_SIZE, -y * TILE_SIZE, 0f);
			}
		}
	}
}
