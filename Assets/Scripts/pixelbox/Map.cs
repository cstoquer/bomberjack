namespace Pixelbox {

	public class Map {
		public string name;
		public int width;
		public int height;
		public MapItem[,] items;

		public Map(int width, int height) {
			this.name = "";
			this.width = width;
			this.height = height;
			this.items = new MapItem[width, height];
		}

		public void Remove(int x, int y) {
			this.items[x, y] = null;
		}

		public void Set(int x, int y, int sprite, bool flipH, bool flipV, bool flipR, bool flagA, bool flagB) {
			if (sprite == -1) {
				Remove(x, y);
				return;
			}
			if (x < 0 || y < 0 || x >= width || y >= height) return;
			this.items[x, y] = new MapItem(x, y, sprite, flipH, flipV, flipR, flagA, flagB);
		}

		public MapItem Get(int x, int y) {
			if (x < 0 || y < 0 || x >= this.width || y >= this.height) return null;
			return this.items[x, y];
		}
	}
}
