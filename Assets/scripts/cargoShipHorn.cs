using UnityEngine;
using System.Collections;

public class cargoShipHorn : MonoBehaviour {

	public AudioClip cargoShiPClip;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider col){
		if(col.tag == "Player"){
			GameObject.Find("GameManager").GetComponent<AudioSource>().clip = cargoShiPClip;
			GameObject.Find("GameManager").GetComponent<AudioSource>().Play();
		}
	}
}
