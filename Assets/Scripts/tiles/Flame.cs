using UnityEngine;
using System.Collections;

namespace Bomberman.Tiles {
	public class Flame : Tile {
		private Sprite[] animSprites;

		// when another flame is put on top of this tile, this boolean is set to keep the last
		// added flame and this tile don't attempt to remove itself from stage
		[HideInInspector] public bool overriden = false;

		public int animFrame {
			get { return 0; }
			set { spriteRenderer.sprite = animSprites[value]; }
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		public void Init(Sprite[] animSprites, int i, int j) {
			this.animSprites = animSprites;
			this.i = i;
			this.j = j;
			spriteRenderer = GetComponent<SpriteRenderer>();
			spriteRenderer.sprite = animSprites[0];
			spriteRenderer.sortingOrder = j;
			gameObject.transform.position = new Vector3(i * 16, -j * 16, 0f);
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		public void Remove() {
			if(!overriden) Stage.instance.SetTile(i, j);
			Destroy(gameObject);
		}
	}
}
