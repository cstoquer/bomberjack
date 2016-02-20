using System;
using UnityEngine;
using System.Collections.Generic;

namespace Pixelbox {

	[Serializable]
	public class Maps {
		public MapData[] data;

		private static Map[] maps;
		private static bool loaded = false;
		private static Dictionary<string, Map> mapByName;

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		private static void LoadMaps() {
			if (loaded) return;
			TextAsset textAsset = Resources.Load("maps") as TextAsset;
			string json = "{\"data\":" + textAsset.text + "}";
			Maps mapData = JsonUtility.FromJson<Maps>(json);

			maps = new Map[mapData.data.Length];
			for (int i = 0; i < maps.Length; i++) {
				maps[i] = mapData.data[i].Decode();
			}

			mapByName = new Dictionary<string, Map>();
			for (int i = 0; i < maps.Length; i++) {
				string name = maps[i].name;
				if (name == null || name == "") continue;
				mapByName[name] = maps[i];
			}

			loaded = true;
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		public static Map GetMap(int index) {
			LoadMaps();
			return maps[index];
		}

		//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
		public static Map GetMap(string name) {
			LoadMaps();
			return mapByName[name];
		}
	}
}
