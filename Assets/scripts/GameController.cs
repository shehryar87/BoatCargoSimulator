using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject[] level1Items;
	public GameObject[] level2Items;
	public GameObject[] level3Items;
	public GameObject[] level4Items;
	public GameObject[] level5Items;
	public GameObject[] level6Items;
	public GameObject[] level7Items;
	public GameObject[] level8Items;
	public GameObject[] level9Items;
	public GameObject[] level10Items;
	public GameObject[] level11Items;
	public GameObject[] level12Items;
	public GameObject[] level13Items;
	public GameObject[] level14Items;
	public GameObject[] level15Items;
	public GameObject[] level16Items;
	public GameObject[] level17Items;
	public GameObject[] level18Items;
	public GameObject[] level19Items;
	public GameObject[] level20Items;



	void Start()
	{

	//	AdmobManager.instance.HideBanner(AdmobManager.BannerAD.SmallBanner);
		int startLevel = PlayerPrefs.GetInt("StartShipLevel", 1);
		GameObject van = GameObject.Find("Shipp");

		GameObject[][] levelItems = {
		level1Items, level2Items, level3Items, level4Items, level5Items,
		level6Items, level7Items, level8Items, level9Items, level10Items,
		level11Items, level12Items, level13Items, level14Items, level15Items,
		level16Items, level17Items, level18Items, level19Items, level20Items
	};

		int levelIndex = startLevel - 1;

		if (levelIndex >= 0 && levelIndex < levelItems.Length)
		{
			for (int i = 0; i < levelItems[levelIndex].Length; i++)
			{
				levelItems[levelIndex][i].SetActive(true);
			}

			if (levelItems[levelIndex].Length > 1)
			{
				van.transform.position = levelItems[levelIndex][1].transform.position;
				van.transform.rotation = levelItems[levelIndex][1].transform.rotation;
			}
		}
	}


	
}
