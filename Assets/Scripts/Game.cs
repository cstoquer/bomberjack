using UnityEngine;
using System.Collections;
using Bomberman.Entities;

namespace Bomberman {
	public class Game : MonoBehaviour {
		public GameObject stagePrefab;
		public GameObject[] playerPrefabs;

		void Start() {
			GameObject instance = Instantiate(stagePrefab);
			Stage stage = instance.GetComponent<Stage>();
			stage.Init("classic");

			// spawnpoints are the 4 corners of the stage inside a 1 tile surounding wall
			Vector2[] spawnpoints = new Vector2[4];
			spawnpoints[0] = new Vector2(1, 1);
			spawnpoints[1] = new Vector2(stage.width - 2, 1);
			spawnpoints[2] = new Vector2(1, stage.height - 2);
			spawnpoints[3] = new Vector2(stage.width - 2, stage.height - 2);

			for (int i = 0; i < 4; i++) {
				GameObject player = Instantiate(playerPrefabs[i]);
				player.GetComponent<Player>().Init(i + 1, spawnpoints[i]);
			}
		}
	}
}
