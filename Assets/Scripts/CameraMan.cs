using UnityEngine;
using System.Collections;

public class CameraMan : MonoBehaviour {
	private const int TILE_SIZE = 16;

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	void Start() {
		int width  = Screen.width;
		//int height = Screen.height;

		int PIXEL_SIZE = width / (15 * TILE_SIZE);

		GetComponent<Camera>().orthographicSize = (float)(Screen.height) / (float)(2 * PIXEL_SIZE);

		{
			// HACK: force rendering to be 60 FPS
			Application.targetFrameRate = 60;
			Resolution res;
			res = Screen.currentResolution;
			if (res.refreshRate == 60)  QualitySettings.vSyncCount = 1;
			if (res.refreshRate == 120) QualitySettings.vSyncCount = 2;
			//Debug.Log("res.refreshRate =" + (int)(res.refreshRate));
			//Debug.Log("vSyncCount = " + (int)(QualitySettings.vSyncCount));
		}
	}
}
