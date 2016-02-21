using UnityEngine;
using System.Collections;
using Pixelbox;

namespace Bomberman.Tiles {
	public class Bomb : Destructible {
		private const int DELAY = 6; // delay between bomb trigger and explosion

		private int flameSize;
		private int timer;

		public GameObject explosionPrefab;

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		public override void Init(MapItem item, Stage stage) {
			base.Init(item, stage);
			flameSize = 4;
			timer = 120 + Random.Range(0, 120);
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		public void Init(int i, int j, int flameSize, int timer) {
			this.i = i;
			this.j = j;
			this.flameSize = flameSize;
			this.timer = timer;
			this.stage = Stage.instance;

			spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
			spriteRenderer.sortingOrder = j;
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
			stage.SetTile(i, j);

			GameObject instance = (GameObject)Instantiate(explosionPrefab, transform.position, Quaternion.identity);
			instance.GetComponent<Explosion>().Init(i, j, flameSize);
			Destroy(gameObject);
		}
	}
}
