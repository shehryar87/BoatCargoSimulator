using UnityEngine;
using System.Collections;

public class CargoTarget : MonoBehaviour {

	public GameObject childComponent;
	public GameObject mainCamera;
	public GameObject camInside;
	public GameObject stopText;
	public GameObject crates;
	
	private GameObject touchManager;
	bool once;
	bool onceAgain;
	bool packageLoaded;
	float defaultHeight;
	float defaultDistance;

//	public bool changCam;

	void Start () {
		stopText.SetActive (false);
		touchManager = GameObject.Find ("_TouchManagerUgui");
		once = false;
		onceAgain = false;
		packageLoaded = false;
	
		defaultHeight = mainCamera.GetComponent<RCCCarCamera>().height;
		defaultDistance = mainCamera.GetComponent<RCCCarCamera>().distance;
		Arrow.target = transform;
	}
	
	void Update () {
//		if(changCam){
//			StartCoroutine(changeCamera());
//		}
	}
	
	void OnTriggerStay(Collider col){

		if(crates.activeSelf){
			touchManager.GetComponent<Canvas>().enabled = true;
			packageLoaded = true;
			GameObject.Find("GameManager").GetComponent<StoreController>().enabled = true;
		}

		if (col.tag == "Player" && col.gameObject.GetComponent<Rigidbody> ().velocity.magnitude < 0.6f && !onceAgain) {
			StopCoroutine(Alert());
			col.gameObject.GetComponent<Rigidbody> ().velocity = col.gameObject.GetComponent<Rigidbody> ().velocity * 0.1f;
			touchManager.GetComponent<Canvas>().enabled = false;
			StartCoroutine(changeCamera());
			stopText.SetActive(false);
			childComponent.SetActive(false);
			onceAgain = true;
		} 
		else if(col.tag == "Player" && col.gameObject.GetComponent<Rigidbody> ().velocity.magnitude > 1.0f && !once && !packageLoaded) {		
			StartCoroutine(Alert());
			touchManager.GetComponent<Canvas>().enabled = true;
			once = true;
			mainCamera.GetComponent<RCCCarCamera>().useSmoothRotation = true;
			mainCamera.GetComponent<RCCCarCamera>().moveCam = false;
			}
	}
	
	void OnTriggerExit(Collider col){
		if (col.tag == "Player") {
			mainCamera.GetComponent<RCCCarCamera>().useSmoothRotation = true;
			mainCamera.GetComponent<RCCCarCamera>().moveCam = false;
			touchManager.GetComponent<Canvas>().enabled = true;
			stopText.SetActive(false);
			if(!packageLoaded){
				childComponent.SetActive(true);
			}
		}
	}

	IEnumerator changeCamera(){
		camInside.SetActive (false);
		mainCamera.SetActive (true);
		mainCamera.GetComponent<RCCCarCamera>().useSmoothRotation = false;
		mainCamera.GetComponent<RCCCarCamera>().height = 2.5f;
		mainCamera.GetComponent<RCCCarCamera>().distance = 4.5f;
		mainCamera.GetComponent<RCCCarCamera> ().rotationDamping = 0.5f;
		mainCamera.GetComponent<RCCCarCamera>().moveCam = true;
		yield return new WaitForSeconds (2.0f);
		mainCamera.GetComponent<RCCCarCamera>().moveCam = false;
		mainCamera.GetComponent<RCCCarCamera>().moveCam2 = true;
		yield return new WaitForSeconds (2.0f);
		crates.SetActive(true);
		mainCamera.GetComponent<RCCCarCamera>().moveCam2 = false;
		mainCamera.GetComponent<RCCCarCamera>().moveCam3 = true;
		yield return new WaitForSeconds (2.0f);
	//	Debug.Log (1);
		mainCamera.GetComponent<RCCCarCamera>().moveCam3 = false;
		mainCamera.GetComponent<RCCCarCamera>().useSmoothRotation = true;
		mainCamera.GetComponent<RCCCarCamera> ().height = defaultHeight;
		mainCamera.GetComponent<RCCCarCamera> ().distance = defaultDistance;
		gameObject.SetActive(false);
	}
	
	IEnumerator Alert(){
		stopText.SetActive(true);
		yield return new WaitForSeconds (0.7f);
		stopText.SetActive(false);
		yield return new WaitForSeconds (0.7f);
		stopText.SetActive(true);
		yield return new WaitForSeconds (0.7f);
		stopText.SetActive(false);
		yield return new WaitForSeconds (0.7f);
		stopText.SetActive(true);
		yield return new WaitForSeconds (0.7f);
		stopText.SetActive(false);
		yield return new WaitForSeconds (0.7f);
		stopText.SetActive(true);
		yield return new WaitForSeconds (0.7f);
		stopText.SetActive(false);
		
	}

}
