using UnityEngine;
using System.Collections;

public class StoreController : MonoBehaviour {

	public GameObject[] dropPoint;

	private int startLevel;

	void Start () {
		startLevel = PlayerPrefs.GetInt ("StartShipLevel",1);

		switch(startLevel){

		case 1:
			dropPoint[0].SetActive(true);
			break;
		case 2:
			dropPoint[1].SetActive(true);
			break;
		case 3:
			dropPoint[2].SetActive(true);
			break;
		case 4:
			dropPoint[3].SetActive(true);
			break;
		case 5:
			dropPoint[4].SetActive(true);
			break;
		case 6:
			dropPoint[5].SetActive(true);
			break;
		case 7:
			dropPoint[6].SetActive(true);
			break;
		case 8:
			dropPoint[7].SetActive(true);
			break;
		case 9:
			dropPoint[8].SetActive(true);
			break;
		case 10:
			dropPoint[9].SetActive(true);
			break;
		case 11:
			dropPoint[10].SetActive(true);
			break;
		case 12:
			dropPoint[11].SetActive(true);
			break;
		case 13:
			dropPoint[12].SetActive(true);
			break;
		case 14:
			dropPoint[13].SetActive(true);
			break;
		case 15:
			dropPoint[14].SetActive(true);
			break;
		case 16:
			dropPoint[15].SetActive(true);
			break;
		case 17:
			dropPoint[16].SetActive(true);
			break;
		case 18:
			dropPoint[17].SetActive(true);
			break;
		case 19:
			dropPoint[18].SetActive(true);
			break;
		case 20:
			dropPoint[19].SetActive(true);
			break;

		}
	
	}
	
	void Update () {
	
	}
}
