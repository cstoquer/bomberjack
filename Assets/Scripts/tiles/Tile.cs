﻿using UnityEngine;
using Pixelbox;
using Bomberman.Entities;

namespace Bomberman.Tiles {
	public enum PowerupCode {
		NULL,
		BOMB,
		FLAME,
		SPEED,
		PUNCH,
		THROW,
		KICK
	}

	public class Tile : MonoBehaviour {
		protected const int TILE_SIZE = 16;

		[HideInInspector] public int i;
		[HideInInspector] public int j;
		
		public bool isEmpty;
		public bool isExplodable;
		public bool isWalkable;

		protected SpriteRenderer spriteRenderer;
		protected Stage stage;


		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		void Start() {
			spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		public Tile(int i = 0, int j = 0, bool isEmpty = false, bool isExplodable = false, bool isWalkable = true) {
			this.i = i;
			this.j = j;
			this.isEmpty = isEmpty;
			this.isExplodable = isExplodable;
			this.isWalkable = isWalkable;
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		public virtual void Init(MapItem item, Stage stage) {
			this.i = item.x;
			this.j = item.y;
			this.stage = stage;

			spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
			spriteRenderer.sortingOrder = j;
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		public virtual void Explode() {
			// overriten in Destructible
			// Remove();
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		public void Remove() {
			Stage.instance.RemoveTile(i, j, this);
			Destroy(gameObject);
		}
	}
}
