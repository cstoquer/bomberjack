using UnityEngine;
using System.Collections;

public class CameraMan : MonoBehaviour {
	private const int TILE_SIZE = 16;

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	void Start() {
		// HACK: force rendering to be 60 FPS
		Application.targetFrameRate = 60;
		Resolution res;
		res = Screen.currentResolution;
		if (res.refreshRate == 60)  QualitySettings.vSyncCount = 1;
		if (res.refreshRate == 120) QualitySettings.vSyncCount = 2;
		//Debug.Log("res.refreshRate =" + (int)(res.refreshRate));
		//Debug.Log("vSyncCount = " + (int)(QualitySettings.vSyncCount));
	}

	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	public void InitPosition(Stage stage) {
		int width  = Screen.width;
		int height = Screen.height;

		int pixelWidth  = width  / (stage.width  * TILE_SIZE);
		int pixelHeight = height / (stage.height * TILE_SIZE); // TODO add HUD size
		int pixelSize = Mathf.Min(pixelWidth, pixelHeight);

		GetComponent<Camera>().orthographicSize = (float)(Screen.height) / (float)(2 * pixelSize);
		transform.position = new Vector3(stage.width * TILE_SIZE / 2, -stage.height * TILE_SIZE / 2, -10); // TODO HUD
	}
}
