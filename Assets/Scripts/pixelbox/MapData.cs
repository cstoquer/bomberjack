using UnityEngine;
using System;
using System.Collections.Generic;

namespace Pixelbox {

	[Serializable]
	public class MapData {
		public int w;
		public int h;
		public string name;
		public string data;

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		private const string BASE = "#$%&'()*+,-~/0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[]^_`abcdefghijklmnopqrstuvwxyz{|}. !";
		private static Dictionary<char, int> INVERSE;
		private static bool isReady = false;
		private static int LENGTH = BASE.Length;
		private static int NULL = LENGTH * LENGTH - 1;
		private static int DUPL = (int)Mathf.Pow(2, 13);
		//private static int MAX_DUPL = NULL - DUPL - 1;

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		private static void Prepare() {
			INVERSE = new Dictionary<char, int>();
			for (int i = 0; i < BASE.Length; i++) {
				INVERSE[BASE[i]] = i;
			}
			isReady = true;
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		private int[] DecodeData(string str) {
			if (!isReady) Prepare();
			List<int> arr = new List<int>();
			for (int i = 0; i < str.Length;) {
				char be = str[i++];
				char le = str[i++];
				int value = INVERSE[be] * LENGTH + INVERSE[le];
				if (value == NULL) {
					arr.Add(-1);
				} else if (value > DUPL) {
					var count = value - DUPL;
					var duplicate = arr[arr.Count - 1];

					for (var j = 0; j < count; j++) {
						arr.Add(duplicate);
					}
				} else {
					arr.Add(value);
				}
			}
			return arr.ToArray();
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		public Map Decode() {
			Map map = new Map(w, h);
			map.name = this.name;
			int[] arr = DecodeData(data);
			for (int x = 0; x < w; x++) {
				for (int y = 0; y < h; y++) {
					int d = arr[x + y * w];
					if (d == -1) continue;
					int sprite = d & 255;
					bool flipH = ((d >> 8) & 1) == 1;
					bool flipV = ((d >> 9) & 1) == 1;
					bool flipR = ((d >> 10) & 1) == 1;
					int flagA = ((d >> 11) & 1);
					int flagB = ((d >> 12) & 1);
					map.Set(x, y, sprite, flipH, flipV, flipR, flagA, flagB);
				}
			}
			return map;
		}
	}
}
