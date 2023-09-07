using UnityEngine;
using System.Collections;

public class CurrentWayPoint : MonoBehaviour {

	public Transform currWaypoint = null;

	void Start () {
		GameObject.Find ("Direction").GetComponent<ArrowDirection>().SetCurrentWaypoint(currWaypoint);
	}
	
	void Update () {
	
	}
}
