using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Vector2 Resolution = new Vector2 (16, 9);
	public static float unitsPerUnit = 1f;
	public static float pixelsPerUnit = 100f;

	public Camera camera;

	private float scale = 1f;

	public float MinimumCameraSize = 0f;
	public float MaximumCameraSize = 0f;
			
	void Awake(){
		this.Resolution *= (unitsPerUnit * pixelsPerUnit);

		this.camera = GetComponent<Camera> ();
		if (camera.orthographic) {
			this.scale = Screen.height / Resolution.y;
			pixelsPerUnit *= this.scale;
			camera.orthographicSize = (Screen.height / 2.0f) / pixelsPerUnit;
			this.MinimumCameraSize = this.camera.orthographicSize / 2.0f;
			this.MaximumCameraSize = this.camera.orthographicSize * 1.5f;
		}
		Debug.Log (Screen.width);
		Debug.Log (Screen.height);
		Debug.Log (scale);
		Debug.Log (camera.orthographicSize);
	}
	IEnumerator CameraUp(){
		for (float i=0; i<1f; i+= 0.1f) {
			if (this.camera.orthographicSize <= this.MaximumCameraSize) {
				this.camera.orthographicSize += 0.1f;
			}
			yield return null;
		}
	}
	
	IEnumerator CameraDown()
	{
		for (float i=0; i<1f; i+= 0.1f) {
			if (this.camera.orthographicSize >= this.MinimumCameraSize) {
				this.camera.orthographicSize -= 0.1f;
			}
			yield return null;
		}
	}
	IEnumerator CameraMove()
	{
		return null;
	}
	
	
	void Update()
	{
		var wheel = Input.GetAxis ("Mouse ScrollWheel");
		if (wheel > 0f) {
			StartCoroutine("CameraUp");
		}
		if (wheel < 0f) {
			StartCoroutine("CameraDown");
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			StartCoroutine("CameraDown");
		}
		if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			StartCoroutine("CameraUp");
		}
	}
}
