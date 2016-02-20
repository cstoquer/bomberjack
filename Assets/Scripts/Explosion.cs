using UnityEngine;
using System.Collections;
using System.Collections.Generic;


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
	private ExplosionElement[] elements;

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	void Start() {
		Create(2, 2, 3);
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	// Use this for initialization
	public void Create(int i, int j, int size) {
		List<ExplosionElement> arr = new List<ExplosionElement>();

		arr.Add(new ExplosionElement(center, i, j));

		for (int s = 1; s < size; s++) {
			AddElement(arr, horizontal, i - s, j);
			AddElement(arr, horizontal, i + s, j);
			AddElement(arr, vertical,   i, j - s);
			AddElement(arr, vertical,   i, j + s);
		}

		AddElement(arr, left,   i - size, j);
            AddElement(arr, right,  i + size, j);
            AddElement(arr, top,    i, j - size);
            AddElement(arr, bottom, i, j + size);

		elements = arr.ToArray();

		animFrame = 0;
		frame = 0;
		gameObject.SetActive(true);
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	private void AddElement(List<ExplosionElement> arr, Sprite[] type, int i, int j) {
		// TODO test that we can add an element in map
		// TODO also if there is a wall add a wall explosion element
		arr.Add(new ExplosionElement(type, i, j));
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	// Update is called once per frame
	void Update () {
		if (++frame > 5) {
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
