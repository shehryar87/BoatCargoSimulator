using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddPinpong : MonoBehaviour {

	public float duration = 1.0f;
	void Start () {
		
	}
	
	void Update () {
		Color textureColor = gameObject.GetComponent<Button> ().image.color;
		textureColor.r = Mathf.PingPong(Time.time, duration) / duration;
		gameObject.GetComponent<Button> ().image.color = textureColor;
	}
}
