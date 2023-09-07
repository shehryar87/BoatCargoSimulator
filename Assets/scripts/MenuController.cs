using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    public GameObject MainMenuPanel;
    public GameObject levelPanel;
    public GameObject settingPanel, exitPanel;
    public AudioClip buttonClick;
    public GameObject MainCanvas;

    public Slider musicSlider;
    public AudioSource musicSource;
    private AudioSource audioSource;

    void Start()
    {

        Time.timeScale = 1;
        audioSource = gameObject.GetComponent<AudioSource>();
        AdmobManager.instance.ShowBanner(AdmobManager.BannerAD.SmallBanner);
       /* if (PlayerPrefs.GetInt("Music") == 0)
        {
            MainCanvas.GetComponent<AudioSource>().mute = false;
        }
        else
        {
            MainCanvas.GetComponent<AudioSource>().mute = true;
        }*/

    }

    public void SoundSlider()
    {
        musicSlider.onValueChanged.AddListener(delegate { ChangeVolume(musicSlider.value); });
    }
    void ChangeVolume(float sliderValue)
    {
        musicSource.volume = sliderValue;
    }


    void ButtonClick()
    {
        audioSource.clip = buttonClick;
        audioSource.Play();
    }
    public void Back()
    {
        ButtonClick();
        settingPanel.SetActive(false);
        levelPanel.SetActive(false);
        exitPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    public void Play()
    {
        Debug.Log("Play");
        ButtonClick();
        MainMenuPanel.SetActive(false);
        levelPanel.SetActive(true);
    }

    public void Like()
    {
        ButtonClick();
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.ig.boat.cargo.transport.game");
    }

    public void Setting()
    {
        ButtonClick();
        MainMenuPanel.SetActive(false);
        settingPanel.SetActive(true);
    }

    public void More()
    {
        ButtonClick();
        Application.OpenURL("https://play.google.com/store/apps/dev?id=5357562518957781163");
    }
    public void OnExitBtn()
    {
        exitPanel.SetActive(true);
    }
    public void Quit()
    {
        ButtonClick();
        Application.Quit();
    }
    public void PlayLevel(int levelNumber)
    {
        AdmobManager.instance.ShowInterstitialAd();
        ButtonClick();
        levelPanel.SetActive(false);
        PlayerPrefs.SetInt("StartShipLevel",levelNumber);
        SceneLoader.instance.LoadNextScene(Scenes.Gameplay);
    }
}
