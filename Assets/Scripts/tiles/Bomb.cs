using UnityEngine;
using System.Collections;
using Pixelbox;
using Bomberman.Entities;

namespace Bomberman.Tiles {
	public class Bomb : Destructible {
		public int DELAY; // delay between bomb trigger and explosion
		public GameObject explosionPrefab;

		[HideInInspector] public Player player; // who drop that bomb

		private int flameSize;
		private int timer;

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		public void Init(int i, int j, int flameSize, int timer, Player player = null) {
			base.Init(i, j);

			this.flameSize = flameSize;
			this.timer = timer;
			this.player = player;
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		void Update() {
			if (isExploding) return;
			timer -= 1;
			if (timer < 0) Explode();
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		protected override IEnumerator ExplosionCoroutine() {

			// wait for exactly DELAY frames before explosion
			for (int c = 0; c < DELAY; c++) yield return null;

			GetComponent<Animator>().Stop();
			stage.RemoveTile(this);

			GameObject instance = (GameObject)Instantiate(explosionPrefab, transform.position, Quaternion.identity);
			instance.GetComponent<Explosion>().Init(i, j, flameSize);

			if (player != null) player.droppedBomb -= 1; // TODO should we wait for the flame end ?

			Destroy(gameObject);
		}
	}
}
