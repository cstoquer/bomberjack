using UnityEngine;
using System.Collections;

namespace Bomberman.Tiles {
	public class Powerup : Destructible {
		public Sprite[] spriteAnim;
		public PowerupCode code;

		private int frame;
		private int frameCounter;

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		void Update() {
			if (isExploding || spriteAnim.Length < 2) return;
			if (++frameCounter < 3) return;
			frameCounter = 0;
			if (++frame >= spriteAnim.Length) frame = 0;
			spriteRenderer.sprite = spriteAnim[frame];
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		public override void Explode() {
			base.Explode();
			isWalkable = false; // when powerup is exploding, forbid to be picked up
		}
	}
}
