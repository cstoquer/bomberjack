using UnityEngine;
using System.Collections;
using Bomberman.Entities;
using UnityEngine.SceneManagement;

namespace Bomberman {
	public class Game : MonoBehaviour {
		public GameObject stagePrefab;
		public GameObject playerPrefab;

		private string[] playerVariations = { "white", "black", "blue", "red" };
		private Player[] players = new Player[4];

		private int alive;

		public static Game instance;

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		void Start() {
			instance = this;

			Stage stage = Instantiate(stagePrefab).GetComponent<Stage>();
			stage.Init("classic");

			// spawnpoints are the 4 corners of the stage inside a 1 tile surounding wall
			Vector2[] spawnpoints = new Vector2[4];
			spawnpoints[0] = new Vector2(1, 1);
			spawnpoints[1] = new Vector2(stage.width - 2, 1);
			spawnpoints[2] = new Vector2(1, stage.height - 2);
			spawnpoints[3] = new Vector2(stage.width - 2, stage.height - 2);

			for (int i = 0; i < 4; i++) {
				Player player = Instantiate(playerPrefab).GetComponent<Player>();
				player.Init(i, spawnpoints[i], playerVariations[i], stage);
				players[i] = player;
			}

			alive = 4;
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		void Update() {
			if (Input.GetKey("escape")) Application.Quit();
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		public void PlayerDeath(Player player) {
			alive -= 1;
			if (alive <= 1) {
				// TODO
				print("game over");
				SceneManager.LoadScene("intro");
			}
		}
	}
}
