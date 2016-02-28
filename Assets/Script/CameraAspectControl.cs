using UnityEngine;
using System.Collections;

public class CameraAspectControl : MonoBehaviour {

	public int orthographicSize;
	float targetAspect = 9.0f / 16.0f;

	void Start()
	{
		ControlCamera ();
	}

	void Update() 
	{
		ControlCamera ();
	}

	private void ControlCamera() {
		ChangeCameraOrthographicSize ();
//				changeCameraRect();
	}

	private void ChangeCameraOrthographicSize() 
	{
		float windowAspect = (float)Screen.width / (float)Screen.height;
		Debug.Log ("windowaspect : " + windowAspect);
		float scaleHeight = windowAspect / targetAspect;
		Debug.Log ("scaleheight : " + scaleHeight);
		Camera camera = GetComponent<Camera>();
		if (scaleHeight < 1.0f) 
		{  
			camera.orthographicSize = orthographicSize / scaleHeight;
		}
	}

	private void changeCameraRect() 
	{
		// determine the game window's current aspect ratio
		float windowAspect = (float)Screen.width / (float)Screen.height;
		Debug.Log ("windowaspect : " + windowAspect);

		// current viewport height should be scaled by this amount
		float scaleHeight = windowAspect / targetAspect;
		Debug.Log ("scaleheight : " + scaleHeight);

		// obtain camera component so we can modify its viewport
		Camera camera = GetComponent<Camera> ();

		// if scaled height is less than current height, add letterbox
		if (scaleHeight < 1.0f) 
		{
			Rect rect = camera.rect;

			rect.width = 1.0f;
			rect.height = scaleHeight;
			rect.x = 0;
			rect.y = (1.0f - scaleHeight) / 2.0f;

			camera.rect = rect;
		} else 
		{ // add pillarbox
			float scalewidth = 1.0f / scaleHeight;

			Rect rect = camera.rect;

			rect.width = scalewidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scalewidth) / 2.0f;
			rect.y = 0;

			camera.rect = rect;
		}
	}
}
