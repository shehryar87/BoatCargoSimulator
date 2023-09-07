using UnityEngine;
using System.Collections;

public class PassengerTarget : MonoBehaviour {

	public GameObject childComponent;
	public GameObject islandCamera;
	public GameObject mainCamera;
	public GameObject camInside;
	public static int count;
	public GameObject stopText;

	private GameObject touchManager;
	bool once;

	void Start () {
		stopText.SetActive (false);
		touchManager = GameObject.Find ("_TouchManagerUgui");
		once = false;
		count = 0;
		Arrow.target = transform;
	}
	
	void Update () {

	}

	void OnTriggerStay(Collider col){

		if(count >= 3){
			islandCamera.SetActive(false);
			mainCamera.SetActive(true);
			touchManager.GetComponent<Canvas>().enabled = true;
			gameObject.SetActive(false);
			GameObject.Find("GameManager").GetComponent<StoreController>().enabled = true;
		}

		if (col.tag == "Player" && col.gameObject.GetComponent<Rigidbody> ().velocity.magnitude < 0.6f && count < 3) {
			StopCoroutine(Alert());
			col.gameObject.GetComponent<Rigidbody> ().velocity = col.gameObject.GetComponent<Rigidbody> ().velocity * 0.7f;
			islandCamera.SetActive(true);
			camInside.SetActive (false);
			mainCamera.SetActive(false);
			touchManager.GetComponent<Canvas>().enabled = false;
			stopText.SetActive(false);
			PassengersAI.move = true;
			childComponent.SetActive(false);
		} 
		else if(col.tag == "Player" && col.gameObject.GetComponent<Rigidbody> ().velocity.magnitude > 1.0f && !once && count < 3) {		
			StartCoroutine(Alert());
				PassengersAI.move = false;
				once = true;
				touchManager.GetComponent<Canvas>().enabled = true;
				islandCamera.SetActive(false);
				mainCamera.SetActive(true);

		}
	}

	void OnTriggerExit(Collider col){
		if (col.tag == "Player") {
			PassengersAI.move = false;
			touchManager.GetComponent<Canvas>().enabled = true;
			stopText.SetActive(false);
			islandCamera.SetActive(false);
			mainCamera.SetActive(true);
			if(count < 3){
				childComponent.SetActive(true);
			}
		}
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
