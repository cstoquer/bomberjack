using UnityEngine;
using System.Collections;

namespace Bomberman.Tiles {
	public class Powerup : Destructible {
		public Sprite[] spriteAnim;
		public PowerupCode code;
	}

	// TODO when powerup explodes, forbid to be picked up ?
}
