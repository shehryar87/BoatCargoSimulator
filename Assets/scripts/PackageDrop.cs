using UnityEngine;
using System.Collections;

public class PackageDrop : MonoBehaviour {

	public GameObject childComponent;
	public GameObject mainCamera;
	public GameObject camInside;
	public GameObject stopText;
	public GameObject crates;
	public int levelNo;

	private int n;
	private GameObject touchManager;
	bool once1;
	bool once2;

	void Start () {
		once1 = false;
		once2 = false;
		stopText.SetActive (false);
		touchManager = GameObject.Find ("_TouchManagerUgui");
		n = PlayerPrefs.GetInt ("shipLevelCompleted",1);
		Arrow.target = transform;
	}
	
	void Update () {
	
	}
	void OnTriggerStay(Collider col){
		if (col.tag == "Player" && col.gameObject.GetComponent<Rigidbody> ().velocity.magnitude < 0.6f && !once1) {
			StopCoroutine(Alert());
			col.gameObject.GetComponent<Rigidbody> ().velocity = col.gameObject.GetComponent<Rigidbody> ().velocity * 0.1f;
			touchManager.GetComponent<Canvas>().enabled = false;
			camInside.SetActive (false);
			mainCamera.SetActive (true);
			StartCoroutine(LeveLCompleteRotation());
			stopText.SetActive(false);
			childComponent.SetActive(false);
			once1 = true;
			GameObject.Find ("GuiController").GetComponent<GuiControler>().LevelComplete();
			if (n <= levelNo) {
				PlayerPrefs.SetInt ("shipLevelCompleted", levelNo+1);
			}
		}
		else if(col.tag == "Player" && col.gameObject.GetComponent<Rigidbody> ().velocity.magnitude > 1.0f && !once2) {		
			StartCoroutine(Alert());
			touchManager.GetComponent<Canvas>().enabled = true;
			once2 = true;
			mainCamera.GetComponent<RCCCarCamera>().useSmoothRotation = true;
			mainCamera.GetComponent<RCCCarCamera>().moveCam = false;
		}
	}

	void OnTriggerExit(Collider col){
		if (col.tag == "Player") {
			mainCamera.GetComponent<RCCCarCamera>().useSmoothRotation = true;
			mainCamera.GetComponent<RCCCarCamera>().moveCam = false;
			touchManager.GetComponent<Canvas>().enabled = true;
			if(!once1){
				StartCoroutine(Alert());
				childComponent.SetActive(true);
			}
		}
	}

	IEnumerator LeveLCompleteRotation(){
		mainCamera.GetComponent<RCCCarCamera>().useSmoothRotation = false;
		mainCamera.GetComponent<RCCCarCamera>().height = 2.5f;
		mainCamera.GetComponent<RCCCarCamera>().distance = 4.5f;
		mainCamera.GetComponent<RCCCarCamera> ().rotationDamping = 0.5f;
		mainCamera.GetComponent<RCCCarCamera>().moveCam = true;
		yield return new WaitForSeconds (1.9f);
		mainCamera.GetComponent<RCCCarCamera>().moveCam = false;
		mainCamera.GetComponent<RCCCarCamera>().moveCam2 = true;
		yield return new WaitForSeconds (1.9f);
		crates.SetActive(false);
		mainCamera.GetComponent<RCCCarCamera>().moveCam2 = false;
		mainCamera.GetComponent<RCCCarCamera>().moveCam3 = true;
		yield return new WaitForSeconds (1.9f);
		mainCamera.GetComponent<RCCCarCamera>().useSmoothRotation = true;
		mainCamera.GetComponent<RCCCarCamera>().moveCam3 = false;
		StartCoroutine(LeveLCompleteRotation());
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
