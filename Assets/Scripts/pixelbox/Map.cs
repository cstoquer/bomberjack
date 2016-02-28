using System.Collections.Generic;

namespace Pixelbox {

	public class Map {
		public string name;
		public int width;
		public int height;
		public MapItem[,] items;

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		public Map(int width, int height) {
			this.name = "";
			this.width = width;
			this.height = height;
			this.items = new MapItem[width, height];
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		public void Remove(int x, int y) {
			this.items[x, y] = null;
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		public void Set(int x, int y, int sprite, bool flipH, bool flipV, bool flipR, int flagA, int flagB) {
			if (sprite == -1) {
				Remove(x, y);
				return;
			}
			if (x < 0 || y < 0 || x >= width || y >= height) return;
			this.items[x, y] = new MapItem(x, y, sprite, flipH, flipV, flipR, flagA, flagB);
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		public MapItem Get(int x, int y) {
			if (x < 0 || y < 0 || x >= this.width || y >= this.height) return null;
			return this.items[x, y];
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		public MapItem[] Find(int sprite, int flagA = 0, int flagB = 0) {
			List<MapItem> arr = new List<MapItem>();
			for (int y = 0; y < height; y++) {
				for (int x = 0; x < width; x++) {
					MapItem item = items[x, y];
					if (item == null) continue;
					if(item.sprite == sprite && item.flagA == flagA && item.flagB == flagB) {
						arr.Add(item);
					}
				}
			}
			return arr.ToArray();
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		public Map Clone() {
			Map map = new Map(width, height);
			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					if (items[x, y] == null) continue;
					map.items[x, y] = new MapItem(items[x, y]);
				}
			}
			return map;
		}
	}
}
