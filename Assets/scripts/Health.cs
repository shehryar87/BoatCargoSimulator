using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {
	
	public Slider healthSlider;
	public GameObject vanInsideCam;
	public GameObject mainCamera;
	public bool isDead; 
	public GameObject flame;
	public Animator deathAnim;
	public GameObject[] destroyObjects;
	public static float healthSliderValue;

	void Start ()
	{
		isDead = false;
	}

    [System.Obsolete]
   /* void Update ()
	{
        if (healthSlider.value <= 0 && !isDead)
        {

            for (int i = 0; i < destroyObjects.Length; i++)
            {
                Destroy(destroyObjects[i]);
            }
            flame.SetActive(true);
            flame.GetComponent<ParticleSystem>().playbackSpeed = 0.4f;
            deathAnim.enabled = true;
            isDead = true;
            vanInsideCam.SetActive(false);
            mainCamera.SetActive(true);
            GameObject.Find("GuiController").GetComponent<GuiControler>().GameOverCrashed();

        }
        healthSliderValue = healthSlider.value;
    }*/


	void Damage(){
		healthSlider.value -= 0.1f;
		if (healthSlider.value <= 0 && !isDead)
		{

			for (int i = 0; i < destroyObjects.Length; i++)
			{
				Destroy(destroyObjects[i]);
			}
			flame.SetActive(true);
			flame.GetComponent<ParticleSystem>().playbackSpeed = 0.4f;
			deathAnim.enabled = true;
			isDead = true;
			vanInsideCam.SetActive(false);
			mainCamera.SetActive(true);
			GameObject.Find("GuiController").GetComponent<GuiControler>().GameOverCrashed();

		}
		healthSliderValue = healthSlider.value;
	}
	


}
