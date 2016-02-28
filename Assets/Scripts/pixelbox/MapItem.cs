namespace Pixelbox {
	public class MapItem {
		public int x;
		public int y;
		public int sprite;
		public bool flipH;
		public bool flipV;
		public bool flipR;
		public int flagA;
		public int flagB;

		public MapItem(int x, int y, int sprite, bool flipH, bool flipV, bool flipR, int flagA, int flagB) {
			this.x = x;
			this.y = y;
			this.sprite = sprite;
			this.flipH = flipH;
			this.flipV = flipV;
			this.flipR = flipR;
			this.flagA = flagA;
			this.flagB = flagB;
		}

		public MapItem(MapItem item) {
			x = item.x;
			y = item.y;
			sprite = item.sprite;
			flipH = item.flipH;
			flipV = item.flipV;
			flipR = item.flipR;
			flagA = item.flagA;
			flagB = item.flagB;
		}
	}
}
