using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelControl : MonoBehaviour
{

	public GameObject[] stars;
//	bool buttomDragIncrease;

	//public TextMesh LevelScoreValue/*, BallsLeftValue, TotalScore, TimeBonusValue, totalFailedScore*/;
	public int LevelScore/*, TotalScoreVariable, TimeBonus*/;
	//public Text ballCountLeft;
	//public float BallsLeftScore;
	//public GameObject LevelBottles;
	//------------------------------------------------
	//public float duration = 1f;
	public int score = 0;
	public int targetScore;
	bool once, starsDataSet;
	//float timer, bottomTimer, TimePassedSeconds, TimePassedMins;
	//GameObject ClockObj;
	//public int BallsScore;
	//[SerializeField] private GameObject Balls;
	//private int total;
	//---------------Upper Score Showing
	// Use this for initialization
	void Start()
	{
	/*	bottomTimer = 1.5f;
		timer = 1.5f;
		once = true;
		targetScore = 1000;
		buttomDragIncrease = false;
		ClockObj = GameObject.FindGameObjectWithTag("clock");
		starsDataSet = false;*/
		SetStarsDataSet();

	}

	// Update is called once per frame
	void Update()
	{
		//Debug.Log("Running Panel", gameObject);
		//SetStarsDataSet();
	//	CheckGameOver();


	}
	public void SetStarsDataSet()
	{

		//   Debug.Log("Running");
		if (!starsDataSet)
		{
			if ((Health.healthSliderValue <= 0.4) /*&& (GameManager.totalBalls >= 2)*/)
			{
				StartCoroutine(ShowStar(1));

			}
			else if ((Health.healthSliderValue <= 0.8) /*&& (GameManager.totalBalls >= 2)*/)

			{
				StartCoroutine(ShowStar(2));
			}
			else /*if ((Health.healthSliderValue > 0.1))*/
            {
				StartCoroutine(ShowStar(3));
			}

			if (once)
			{
				// timer -= Time.deltaTime;
				//if (timer < 0)
			//	OnIncrementScore();
			}
			//TotalScore.text = score.ToString();
		}

	}
	/*void CheckGameOver()
	{
		if (GUICOntroller.GameOver)
		{
			if (once)
			{
				timer -= Time.deltaTime;
				if (timer < 0)
					OnIncrementScore();
			}
			TotalScore.text = score.ToString();
			totalFailedScore.text = score.ToString();
		}
	}*/
	/*public void OnIncrementScore()
	{

		LevelScoreValue.text = GUICOntroller.Score.ToString();
		int bCount = GameManager.totalBalls + GameManager.extraBalls;


		//if (GUICOntroller.LevelComplete) {  } else { LevelScore = 0; }

		BallsLeftScore = (bCount);
		BallsLeftValue.text = (bCount).ToString();


		LevelScore = GUICOntroller.Score;

		if (Clock.seconds <= 10)
		{
			TimeBonus = 300;
		}
		else if (Clock.seconds <= 15)
		{
			TimeBonus = 150;
		}
		else if (Clock.seconds <= 20)
		{
			TimeBonus = 100;
		}
		BallsScore = (bCount) * 1000;
		TimeBonusValue.text = TimeBonus.ToString();

		total = LevelScore + TimeBonus + BallsScore;



		StartCoroutine("CountTo", total);

		once = false;
	}*/
	IEnumerator ShowStar(int count)
	{
		
		/*yield return new WaitForSeconds(2f);*/
		//int levelNumber = PlayerPrefs.GetInt("LevelIndex");
	//	DataSaver.instance.levelHolder.Levels[levelNumber].star = count;
		//	Debug.Log(count);
	//	DataSaver.instance.SaveData();
	//	DataSaver.instance.LoadData();
		for (int i = 0; i < count; i++)
		{
			Debug.Log(count);
			yield return new WaitForSeconds(0.1f);
			stars[i].SetActive(true);
		}
	//	starsDataSet = true;
	}


	IEnumerator CountTo(int target)
	{
		score = (int)Mathf.Lerp(0, target, 3f);
		yield return null;
		score = target;
	}
}
