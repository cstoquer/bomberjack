using UnityEngine;
using System.Collections;

namespace Bomberman.Intro {

	public class Baloon : MonoBehaviour {

		public int speed;

		private int x;
		private int y;

		// Use this for initialization
		void Start() {
			x = (int)(transform.position.x * 1000);
			y = (int)(transform.position.y * 1000);
		}

		// Update is called once per frame
		void Update() {
			x += speed;
			transform.position = new Vector3(x / 1000, y / 1000, 0);
		}
	}
}
