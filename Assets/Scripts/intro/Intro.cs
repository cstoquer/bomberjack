using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Bomberman.Intro {
	public class Intro : MonoBehaviour {

		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {
			// quit
			if (Input.GetKey("escape")) Application.Quit();

			// select
			for (int i = 1; i <= 4; i++) {
				if (Input.GetButtonDown("joy" + i + "_A")) {
					SceneManager.LoadScene("game");
				}
			}
		}
	}
}
