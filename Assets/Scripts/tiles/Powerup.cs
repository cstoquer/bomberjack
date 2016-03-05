using UnityEngine;
using System.Collections;

namespace Bomberman.Tiles {
	public class Powerup : Destructible {
		public Sprite[] spriteAnim;
		public PowerupCode code;

		private int frame;

		// TODO when powerup is exploding, forbid to be picked up ?

		void Update() {
			if (isExploding || spriteAnim.Length < 2) return;
			if (++frame >= spriteAnim.Length) frame = 0;
			spriteRenderer.sprite = spriteAnim[frame];
		}
	}
}
