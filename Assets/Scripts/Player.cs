using UnityEngine;
using System.Collections;

namespace Bomberman.Entities {
	public class Player : MonoBehaviour {

		public int joystick;
		public int speed;

		[HideInInspector] public int i;
		[HideInInspector] public int j;

		private int x;
		private int y;

		private Animator animator;

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		// Use this for initialization
		void Start() {
			animator = GetComponent<Animator>();
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		// Update is called once per frame
		void Update() {
			// read joystick inputs
			bool goR = Input.GetAxisRaw("joy" + joystick + "_H") > 0;
			bool goL = Input.GetAxisRaw("joy" + joystick + "_H") < 0;
			bool goU = Input.GetAxisRaw("joy" + joystick + "_V") > 0;
			bool goD = Input.GetAxisRaw("joy" + joystick + "_V") < 0;

			// movement
			if (goL) x -= speed;
			if (goR) x += speed; 
			if (goU) y -= speed;
			if (goD) y += speed;

			animator.SetBool("goL", goL);
			animator.SetBool("goR", goR);
			animator.SetBool("goU", goU);
			animator.SetBool("goD", goD);

			transform.position = new Vector3(x / 1000, -y / 1000, 0);
		}
	}
}
