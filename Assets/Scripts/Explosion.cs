using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Bomberman.Tiles;


//██████████████████████████████████████████████████████████████████████████████████████████████████████
public class ExplosionElement : Object {
	private Sprite[] animSprites;
	private SpriteRenderer spriteRenderer;

	public GameObject gameObject;
	public int i;
	public int j;

	public int animFrame {
		get { return 0; }
		set { spriteRenderer.sprite = animSprites[value]; }
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	public ExplosionElement(Sprite[] animSprites, int i, int j) {
		this.animSprites = animSprites;
		this.i = i;
		this.j = j;
		gameObject = new GameObject("explosion");
		spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
		spriteRenderer.sprite = animSprites[0];
		spriteRenderer.sortingOrder = j;
		gameObject.transform.position = new Vector3(i * 16, -j * 16, 0f);
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	public void Remove() {
		Destroy(gameObject);
	}
}

//██████████████████████████████████████████████████████████████████████████████████████████████████████
public class Explosion : MonoBehaviour {
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
	private ExplosionElement[] elements = new ExplosionElement[0];

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	// Use this for initialization
	public void Init(int i, int j, int size) {
		if (Stage.instance == null) return;

		List<ExplosionElement> arr = new List<ExplosionElement>();

		//AddElement(arr, center, i, j);

		bool t = true;
		bool b = true;
		bool r = true;
		bool l = true;

		for (int s = 1; s < size; s++) {
			l = l && AddElement(arr, horizontal, i - s, j);
			r = r && AddElement(arr, horizontal, i + s, j);
			b = b && AddElement(arr, vertical,   i, j - s);
			t = t && AddElement(arr, vertical,   i, j + s);
		}

		l = l && AddElement(arr, left,   i - size, j);
		r = r && AddElement(arr, right,  i + size, j);
		b = b && AddElement(arr, top,    i, j - size);
		t = t && AddElement(arr, bottom, i, j + size);

		elements = arr.ToArray();

		animFrame = 0;
		frame = 0;
		gameObject.SetActive(true);
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	private bool AddElement(List<ExplosionElement> arr, Sprite[] type, int i, int j) {
		// test that we can add an element in map
		Stage stage = Stage.instance;
		if (i < 0 || j < 0 || i >= stage.width || j >= stage.height) return false;

		Tile tile = stage.GetTile(i, j);

		if (tile == null) {
			ExplosionElement element = new ExplosionElement(type, i, j);
			element.gameObject.transform.SetParent(transform);
			arr.Add(element);
			return true;
		}

		// check tile is destructible
		if (tile.isExplodable) tile.Explode();

		// block fire stream at this point
		return false;
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	// Update is called once per frame
	void Update () {
		if (++frame >= 5) {
			frame = 0;
			if (++animFrame >= 5) {
				Remove();
				return;
			}
			for (int i = 0; i < elements.Length; i++) {
				elements[i].animFrame = animFrame;
			}
		}
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	private void Remove() {
		for (int i = 0; i < elements.Length; i++) {
			elements[i].Remove();
		}
		gameObject.SetActive(false);
	}
}
