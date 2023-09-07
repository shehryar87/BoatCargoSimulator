using UnityEngine;
using UnityEngine.UI;

public class InputShifter : MonoBehaviour
{
    public GameObject Buttons, SteeringWheel;
    public Image steerImage;
    public Sprite steeringSprite, buttonsSprite;
    private void Start()
    {
        //AdmobManager.instance.ShowBanner(AdmobManager.BannerAD.SmallBanner);
        var input = PlayerPrefs.GetInt("Steer", 0);
        if (input == 0)
        {
            SteeringWheel.SetActive(true);
            Buttons.SetActive(false);

            GetComponent<BoatController>().isUsingSteering = true;
            steerImage.sprite = buttonsSprite;
        }
        else
        {
            SteeringWheel.SetActive(false);
            Buttons.SetActive(true);

            GetComponent<BoatController>().isUsingSteering = false;
            steerImage.sprite = steeringSprite;
        }

    }
    public void ChangeInput()
    {
        Buttons.SetActive(!Buttons.activeSelf);

        SteeringWheel.SetActive(!SteeringWheel.activeSelf);
        bool steer = GetComponent<BoatController>().isUsingSteering;
        GetComponent<BoatController>().isUsingSteering = !steer;
        if (!steer)
        {
            PlayerPrefs.SetInt("Steer", 0);
            steerImage.sprite = buttonsSprite;
        }
        else
        {
            PlayerPrefs.SetInt("Steer", 1);
            steerImage.sprite = steeringSprite;
        }
    }
}
