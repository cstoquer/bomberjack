using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Bomberman.Tiles;


public class Explosion : MonoBehaviour {
	public GameObject flamePrefab;

	// all sprites to render and animate an explosion
	public Sprite[] center;
	public Sprite[] horizontal;
	public Sprite[] vertical;
	public Sprite[] top;
	public Sprite[] right;
	public Sprite[] bottom;
	public Sprite[] left;

	private int animFrame;
	private int frame;
	private Flame[] flames = new Flame[0];

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	// Use this for initialization
	public void Init(int i, int j, int size) {
		if (Stage.instance == null) return;

		List<Flame> arr = new List<Flame>();

		AddFlame(arr, center, i, j);

		bool t = true;
		bool b = true;
		bool r = true;
		bool l = true;

		for (int s = 1; s < size; s++) {
			l = l && AddFlame(arr, horizontal, i - s, j);
			r = r && AddFlame(arr, horizontal, i + s, j);
			b = b && AddFlame(arr, vertical,   i, j - s);
			t = t && AddFlame(arr, vertical,   i, j + s);
		}

		l = l && AddFlame(arr, left,   i - size, j);
		r = r && AddFlame(arr, right,  i + size, j);
		b = b && AddFlame(arr, top,    i, j - size);
		t = t && AddFlame(arr, bottom, i, j + size);

		flames = arr.ToArray();

		animFrame = 0;
		frame = 0;
		gameObject.SetActive(true);
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	private bool AddFlame(List<Flame> arr, Sprite[] type, int i, int j) {
		// test that we can add an element in map
		Stage stage = Stage.instance;
		if (i < 0 || j < 0 || i >= stage.width || j >= stage.height) return false;

		Tile tile = stage.GetTile(i, j);

		if (tile == null) {
			stage.SetTile(i, j, CreateFlame(arr, type, i, j));
			return true;
		}
		
		if (tile.GetType() == typeof(Flame)) {
			Flame flame = (Flame)tile;
			flame.overriden = true;
			// keep the newest flame in Stage
			stage.SetTile(i, j, CreateFlame(arr, type, i, j));
			return true;
		}

		// check if tile is destructible
		if (tile.isExplodable) tile.Explode();

		// block fire stream at this point
		return false;
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	private Flame CreateFlame(List<Flame> arr, Sprite[] type, int i, int j) {
		Flame flame = Instantiate(flamePrefab).GetComponent<Flame>();
		flame.Init(type, i, j);
		flame.gameObject.transform.SetParent(transform);
		arr.Add(flame);
		return flame;
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	// Update is called once per frame
	void Update () {
		if (++frame < 3) return;
		frame = 0;
		if (++animFrame >= 9) {
			Remove();
			return;
		}
		for (int i = 0; i < flames.Length; i++) {
			flames[i].animFrame = animFrame;
		}
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	private void Remove() {
		for (int i = 0; i < flames.Length; i++) {
			flames[i].Remove();
		}
		gameObject.SetActive(false);
		Destroy(gameObject); // TODO keep object in a pool for reuse
	}
}
