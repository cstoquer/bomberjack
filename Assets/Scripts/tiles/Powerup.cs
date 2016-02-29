using UnityEngine;
using System.Collections;

namespace Bomberman.Tiles {
	public enum PowerupCode {
		BOMB,
		FLAME,
		SPEED,
		PUNCH,
		THROW,
		KICK
	}

	public class Powerup : Destructible {
		public PowerupCode code;
	}
}
