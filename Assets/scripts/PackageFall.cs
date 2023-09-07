using UnityEngine;
using System.Collections;

public class PackageFall : MonoBehaviour {
	
	public AudioClip waterCrashSound;

	bool once;

	void Start () {
		once = false;	
	}
	
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll){
		
		if(coll.gameObject.tag == "Crate" && !once){

			GameObject.Find("GameManager").GetComponent<AudioSource>().clip = waterCrashSound;
			GameObject.Find ("GameManager").GetComponent<AudioSource> ().Play ();
			GameObject.Find ("GuiController").GetComponent<GuiControler>().GameOverDrop();
			once = true;
		}
	}
}
