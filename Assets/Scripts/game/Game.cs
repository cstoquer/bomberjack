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

		private int alivePlayerCount;

		public static Game instance;

		[Header("STAGE")]
		public string stageId;

		[Range(0, 1)] public float EMPTY_PERCENT;

		[Header("POWER UP")]
		public int BOMB;
		public int FLAME;
		public int SPEED;
		public int PUNCH;
		public int KICK;
		public int SUPER_FLAME;

		[Header("GAME CONTANTS")]
		public int MAX_BOMB;
		public int MAX_FLAME;
		public int MAX_SPEED;
		public int SPEED_INC;


		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		void Start() {
			instance = this;

			Stage stage = Instantiate(stagePrefab).GetComponent<Stage>();
			stage.Init(stageId);

			for (int i = 0; i < 4; i++) {
				Player player = Instantiate(playerPrefab).GetComponent<Player>();
				player.Init(i, stage.spawnpoints[i], playerVariations[i], stage);
				players[i] = player;
			}

			alivePlayerCount = 4;
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		void Update() {
			if (Input.GetKey("escape")) Application.Quit();
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		public void PlayerDeath(Player player) {
			// FIXME avoid starting coroutine more than once
			if (--alivePlayerCount <= 1) StartCoroutine(GameoverCoroutine());
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		protected IEnumerator GameoverCoroutine() {
			// TODO wait few frame to make sur it's not a draw
			// TODO set last player invincible
			// TODO bomb should not explode anymore

			yield return new WaitForSeconds(4.0f);

			// TODO go to scoreboard screen
			SceneManager.LoadScene("intro");
		}
	}
}
