using UnityEngine;
using System.Collections;

namespace Bomberman.Tiles {
	public class Destructible : Tile {

		public Sprite[] destructAnim;

		protected bool isExploding = false;

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		public override void Explode() {
			if (isExploding) return;
			isExploding = true;
			StartCoroutine(ExplosionCoroutine());
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		protected virtual IEnumerator ExplosionCoroutine() {
			for (int f = 0; f < destructAnim.Length; f++) {
				spriteRenderer.sprite = destructAnim[f];
				for (int c = 0; c < 5; c++) {
					// wait for exactly one frame
					yield return null;
				}
			}

			// TODO item spawning

			stage.RemoveTile(i, j);
			Destroy(gameObject);
		}
	}
}
