namespace Pixelbox {
	public class MapItem {
		public int x;
		public int y;
		public int sprite;
		public bool flipH;
		public bool flipV;
		public bool flipR;
		public bool flagA;
		public bool flagB;

		public MapItem(int x, int y, int sprite, bool flipH, bool flipV, bool flipR, bool flagA, bool flagB) {
			this.x = x;
			this.y = y;
			this.sprite = sprite;
			this.flipH = flipH;
			this.flipV = flipV;
			this.flipR = flipR;
			this.flagA = flagA;
			this.flagB = flagB;
		}
	}
}
