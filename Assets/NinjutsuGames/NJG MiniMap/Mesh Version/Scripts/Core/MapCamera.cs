using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MapCamera : MonoBehaviour {

	public bool canDraw = true;

	float mSize = 0;
	Camera cam;

	void Awake(){
		cam = gameObject.GetComponent<Camera> ();
	}

	void Update()
	{
		mSize = Screen.height / 2.0f;

		if(cam.orthographicSize != mSize)
			cam.orthographicSize = mSize;
	}
}
