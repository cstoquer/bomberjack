using UnityEngine;
using System.Collections;
using Bomberman.Tiles;

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
			x = 26000; // TODO
			y = 26000; // TODO
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		// Update is called once per frame
		void Update() {
			// read joystick inputs
			bool goR = Input.GetAxisRaw("joy" + joystick + "_H") > 0;
			bool goL = Input.GetAxisRaw("joy" + joystick + "_H") < 0;
			bool goU = Input.GetAxisRaw("joy" + joystick + "_V") > 0;
			bool goD = Input.GetAxisRaw("joy" + joystick + "_V") < 0;

			// update animation
			animator.SetBool("goL", goL);
			animator.SetBool("goR", goR);
			animator.SetBool("goU", goU);
			animator.SetBool("goD", goD);

			// movement
			int sx = 0;
			int sy = 0;

			if (goL) sx -= speed;
			if (goR) sx += speed; 
			if (goU) sy -= speed;
			if (goD) sy += speed;

			int tx = x + sx;
			int ty = y + sy;

			Stage stage = Stage.instance;
			Tile tile;

			if (sx > 0) {
				tile = stage.GetTile((tx + 8000) / 16000, y / 16000);
				//if (tile != null) tile.GetComponent<SpriteRenderer>().color = Color.red;
				if (tile != null && !tile.isWalkable) {
					// snap player to the border of the tile
					tx = ((tx + 8000) / 16000) * 16000 - 8000; 
				}
			} else if (sx < 0) {
				tile = stage.GetTile((tx - 8000) / 16000, y / 16000);
				//if (tile != null) tile.GetComponent<SpriteRenderer>().color = Color.yellow;
				if (tile != null && !tile.isWalkable) {
					// snap player to the border of the tile
					tx = ((tx - 8000) / 16000 + 1) * 16000 + 8000;
				}
			}

			if (sy > 0) {
				tile = stage.GetTile(x / 16000, (ty + 8000) / 16000);
				//if (tile != null) tile.GetComponent<SpriteRenderer>().color = Color.green;
				if (tile != null && !tile.isWalkable) {
					// snap player to the border of the tile
					ty = ((ty + 8000) / 16000) * 16000 - 8000;
				}
			} else if (sy < 0) {
				tile = stage.GetTile(x / 16000, (ty - 8000) / 16000);
				//if (tile != null) tile.GetComponent<SpriteRenderer>().color = Color.cyan;
				if (tile != null && !tile.isWalkable) {
					// snap player to the border of the tile
					ty = ((ty - 8000) / 16000 + 1) * 16000 + 8000;
				}
			}

			x = tx;
			y = ty;

			// fetch sprite position
			transform.position = new Vector3(x / 1000, -y / 1000 + 22, 0);

			// bomb dropping
			if (Input.GetButtonDown("joy" + joystick + "_A")) {
				int i = x / 16000;
				int j = y / 16000;
				tile = stage.GetTile(i, j);
				if (tile == null) {
					stage.AddTile(i, j, 2); // FIXME
					((Bomb)stage.GetTile(i, j)).Init(i, j, 5, 120); // FIXME
				}
			}
		}
	}
}
