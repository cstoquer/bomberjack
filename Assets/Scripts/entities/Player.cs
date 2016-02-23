using UnityEngine;
using System.Collections;
using Bomberman.Tiles;

namespace Bomberman.Entities {
	public class Player : MonoBehaviour {

		private const int PIXEL = 1000;
		private const int WIDTH = 8000;
		private const int TILE = 16000;
		private const int FACE = 7600;
		private const int SIDE = 7400;

		public int speed;

		[HideInInspector] public int i;
		[HideInInspector] public int j;
		[HideInInspector] private int x;
		[HideInInspector] private int y;

		private int joystick;
		private Animator animator;
		private Stage stage;

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		public void Init(int joystick, Vector2 spawnpoint) {
			this.joystick = joystick;
			x = WIDTH + TILE * (int)spawnpoint.x;
			y = WIDTH + TILE * (int)spawnpoint.y;
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		// Use this for initialization
		void Start() {
			animator = GetComponent<Animator>();
			stage = Stage.instance;
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

			int ttx = tx;
			int tty = ty;

			Tile tile;
			Tile tileA;
			Tile tileB;

			// TODO avoid bomberman to get stuck on corners when user go in diagonal

			if (sx > 0) {
				int ti = (tx + FACE) / TILE;
				tileA = stage.GetTile(ti, (ty - SIDE) / TILE);
				tileB = stage.GetTile(ti, (ty + SIDE) / TILE);
				if (((tileA != null && !tileA.isWalkable) || (tileB != null && !tileB.isWalkable)) && (x + FACE) / TILE < ti) {
					// if there is a walkable tile then make bomberman slide in front of the entrance
					tile = stage.GetTile(ti, ty / TILE);
					if (tile == null || tile.isWalkable) {
						int direction = ty % TILE < 8000 ? 1 : -1;
						// TODO max speed so to keep player directly in front of entrance
						tty = y + speed * direction;
					}
					// snap player to the border of the tile
					ttx = ti * TILE - WIDTH;
				}
			} else if (sx < 0) {
				int ti = (tx - FACE) / TILE;
				tileA = stage.GetTile(ti, (ty - SIDE) / TILE);
				tileB = stage.GetTile(ti, (ty + SIDE) / TILE);
				if (((tileA != null && !tileA.isWalkable) || (tileB != null && !tileB.isWalkable)) && (x - FACE) / TILE > ti) {
					// if there is a walkable tile then make bomberman slide in front of the entrance
					tile = stage.GetTile(ti, ty / TILE);
					if (tile == null || tile.isWalkable) {
						int direction = ty % TILE < 8000 ? 1 : -1;
						tty = y + speed * direction;
					}
					// snap player to the border of the tile
					ttx = (ti + 1) * TILE + FACE;
				}
			}

			if (sy > 0) {
				int tj = (ty + FACE) / TILE;
				tileA = stage.GetTile((ttx - SIDE) / TILE, tj);
				tileB = stage.GetTile((ttx + SIDE) / TILE, tj);
				if (((tileA != null && !tileA.isWalkable) || (tileB != null && !tileB.isWalkable)) && (y + FACE) / TILE < tj) {
					// if there is a walkable tile then make bomberman slide in front of the entrance
					tile = stage.GetTile(tx / TILE, tj);
					if (tile == null || tile.isWalkable) {
						int direction = tx % TILE < 8000 ? 1 : -1;
						ttx = x + speed * direction;
					}
					// snap player to the border of the tile
					tty = tj * TILE - WIDTH;
				}
			} else if (sy < 0) {
				int tj = (ty - FACE) / TILE;
				tileA = stage.GetTile((ttx - SIDE) / TILE, tj);
				tileB = stage.GetTile((ttx + SIDE) / TILE, tj);
				if (((tileA != null && !tileA.isWalkable) || (tileB != null && !tileB.isWalkable)) && (y - FACE) / TILE > tj) {
					// if there is a walkable tile then make bomberman slide in front of the entrance
					tile = stage.GetTile(tx / TILE, tj);
					if (tile == null || tile.isWalkable) {
						int direction = tx % TILE < 8000 ? 1 : -1;
						ttx = x + speed * direction;
					}
					// snap player to the border of the tile
					tty = (tj + 1) * TILE + FACE;
				}
			}

			x = ttx;
			y = tty;

			// fetch sprite position
			transform.position = new Vector3(x / PIXEL, -y / PIXEL + 22, 0);

			// bomb dropping
			if (Input.GetButtonDown("joy" + joystick + "_A")) {
				int i = x / TILE;
				int j = y / TILE;
				tile = stage.GetTile(i, j);
				if (tile == null) {
					stage.AddTile(i, j, 2); // FIXME
					((Bomb)stage.GetTile(i, j)).Init(i, j, 5, 120); // FIXME
				}
			}
		}
	}
}
