using UnityEngine;

namespace Bomberman.Tiles {
	public class Brick : Destructible {
		[HideInInspector] public PowerupCode powerup; // if brick contain a powerup

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		protected override void SpawnContent() {
			if (powerup == PowerupCode.NULL) return;
			stage.AddPowerup(i, j, powerup);
		}
	}
}
