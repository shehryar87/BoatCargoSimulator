using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Loading : MonoBehaviour 
{
	public Slider _Slider;
	public int loadLevel;
	// Use this for initialization
	void Start () 
	{
		Time.timeScale = 1;
		_Slider.GetComponent<Slider> ().value = 0f;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		_Slider.GetComponent<Slider> ().value += Mathf.Lerp (0, 1, Time.deltaTime * 0.3f);
		if(_Slider.GetComponent<Slider>().value >=1.0f)
		{
			Application.LoadLevel(loadLevel);
		}
	
	}
}
