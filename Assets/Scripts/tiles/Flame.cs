using UnityEngine;
using System.Collections;

namespace Bomberman.Tiles {
	public class Flame : Tile {
		private Sprite[] animSprites;

		public int animFrame {
			get { return 0; }
			set { spriteRenderer.sprite = animSprites[value]; }
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		public void Init(int i, int j, Sprite[] animSprites) {
			base.Init(i, j);
			this.animSprites = animSprites;

			spriteRenderer.sprite = animSprites[0];
			gameObject.transform.position = new Vector3(i * 16, -j * 16, 0f);
		}
	}
}
