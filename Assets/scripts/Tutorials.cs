using UnityEngine;
using System.Collections;

public class Tutorials : MonoBehaviour {

	public GameObject panel1;
	public GameObject panel2;
	public GameObject panel3;
	public GameObject panel4;
	public GameObject panel5;
	public GameObject panel6;
	public GameObject panel7;

	private int activePanel;

	void Start () {
		if (PlayerPrefs.GetInt ("Tutorial") != 0) {
			gameObject.SetActive (false);
			GameObject.Find ("Tuts").SetActive (false);
			Time.timeScale = 1;
		} else {
			Time.timeScale = 0;
			BoatController.mute = true;
			activePanel = 1;
		}
	}
	
	// Update is called once per frame
	void Update () {

		switch(activePanel){

		case 1:
			BoatController.mute = true;
			panel1.SetActive(true);
			panel2.SetActive(false);
			panel3.SetActive(false);
			panel4.SetActive(false);
			panel5.SetActive(false);
			panel6.SetActive(false);
			panel7.SetActive(false);
			break;

		case 2:
			panel1.SetActive(false);
			panel2.SetActive(true);
			panel3.SetActive(false);
			panel4.SetActive(false);
			panel5.SetActive(false);
			panel6.SetActive(false);
			panel7.SetActive(false);
			break;

		case 3:
			panel1.SetActive(false);
			panel2.SetActive(false);
			panel3.SetActive(true);
			panel4.SetActive(false);
			panel5.SetActive(false);
			panel6.SetActive(false);
			panel7.SetActive(false);
			break;

		case 4:
			panel1.SetActive(false);
			panel2.SetActive(false);
			panel3.SetActive(false);
			panel4.SetActive(true);
			panel5.SetActive(false);
			panel6.SetActive(false);
			panel7.SetActive(false);
			break;

		case 5:
			panel1.SetActive(false);
			panel2.SetActive(false);
			panel3.SetActive(false);
			panel4.SetActive(false);
			panel5.SetActive(true);
			panel6.SetActive(false);
			panel7.SetActive(false);
			break;

		case 6:
			panel1.SetActive(false);
			panel2.SetActive(false);
			panel3.SetActive(false);
			panel4.SetActive(false);
			panel5.SetActive(false);
			panel6.SetActive(true);
			panel7.SetActive(false);
			break;

		case 7:
			panel1.SetActive(false);
			panel2.SetActive(false);
			panel3.SetActive(false);
			panel4.SetActive(false);
			panel5.SetActive(false);
			panel6.SetActive(false);
			panel7.SetActive(true);
			break;
		}
	
	}

	public void CloseTutorials(){
		BoatController.mute = false;
		PlayerPrefs.SetInt ("Tutorial", 1);
		gameObject.SetActive (false);
		GameObject.Find ("Tuts").SetActive (false);
		Time.timeScale = 1;
	}

	public void ActivePanel(int i){
		activePanel = i;
	}

	public void LetsGo(){
		BoatController.mute = false;
		PlayerPrefs.SetInt ("Tutorial", 1);
		gameObject.SetActive (false);
		GameObject.Find ("Tuts").SetActive (false);
		Time.timeScale = 1;
	}

}
