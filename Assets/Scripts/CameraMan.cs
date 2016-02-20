using UnityEngine;
using System.Collections;

public class CameraMan : MonoBehaviour {
	private const int TILE_SIZE = 16;

	public float speed;
	public bool constraintBound;
	public bool pixelSnap;

	[Header("Camera Shake")]
	public float friction;
	public float acceleration;

	private float camL;
	private float camR;
	private float camT;
	private float camB;
	
	private Vector3 cameraPos;

	public int PIXEL_SIZE = 3;

	private Vector3 shakeSpeed;
	private Vector3 shakePos;


	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	// Use this for initialization
	void Start() {
		int width  = Screen.width;
		int height = Screen.height;

		PIXEL_SIZE = width / (15 * TILE_SIZE);

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
	//▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
	public void SetBounds(int width, int height) {
		float ratio = (float)(Screen.width) / (float)(Screen.height);
		float cameraHeight = 2 * GetComponent<Camera> ().orthographicSize;
		float cameraWidth = cameraHeight * ratio;
		
		camL = cameraWidth / 2;
		camR = width * TILE_SIZE - cameraWidth / 2;
		camT = cameraHeight / 2;
		camB = height * TILE_SIZE - cameraHeight / 2;
	}
}
