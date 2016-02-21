using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public int i;
	public int j;

	private SpriteRenderer spriteRenderer;

	public void Init(int i, int j) {
		this.i = i;
		this.j = j;

		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		spriteRenderer.sortingOrder = j;
	}
}
