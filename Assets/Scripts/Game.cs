using UnityEngine;
using System.Collections;
using Bomberman.Entities;

namespace Bomberman {
	public class Game : MonoBehaviour {
		public GameObject stagePrefab;
		public GameObject playerPrefab;

		public Vector2[] spawnpoints;

		void Start() {
			GameObject stage = Instantiate(stagePrefab);
			stage.GetComponent<Stage>().Init("classic");

			for (int i = 0; i < 4; i++) {
				GameObject player = Instantiate(playerPrefab);
				player.GetComponent<Player>().Init(i + 1, spawnpoints[i]);
			}
		}
	}
}
