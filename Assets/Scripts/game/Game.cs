using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Bomberman.Entities;

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

			for (int i = 0; i < 4; i++) {
				Player player = Instantiate(playerPrefab).GetComponent<Player>();
				player.Init(i, stage.spawnpoints[i], playerVariations[i], stage);
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
			if (--alive <= 1) StartCoroutine(GameoverCoroutine());
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		protected IEnumerator GameoverCoroutine() {
			// TODO set last player invincible
			// TODO bomb should not explode anymore

			yield return new WaitForSeconds(4.0f);

			// TODO go to scoreboard screen
			SceneManager.LoadScene("intro");
		}
	}
}
