using System.Collections;
using TMPro;
using UnityEngine;

public class GuiControler : MonoBehaviour
{
    public AudioListener ShipAudio;
    public GameObject rccCanvas;
    public GameObject rccMainCam;
    public GameObject gamePause;
    public GameObject gameOverDrop;
    public GameObject levelComplete;
    public GameObject vanInsideCam;

    public AudioClip gameWin;
    public AudioClip gameOver;
    public AudioClip burning;

    private int startLevel;
    private bool toggleCam;
    private bool once;

    public TextMeshProUGUI score;
    int scorePoints = 0;
    public static int starsCount;

    [SerializeField] GameObject[] stars;
    public AudioListener[] myListeners;
    public static bool levelSelection;

    [SerializeField] private GameObject ShipCrashed, LuggageLost;
    //public static int starsCount;
    public TextMeshProUGUI levelNumberText;
    void Start()
    {
        AdmobManager.instance.ShowBanner(AdmobManager.BannerAD.SmallBanner);
        levelSelection = false;
        myListeners = FindObjectsOfType(typeof(AudioListener)) as AudioListener[];
        levelNumberText.text = "LEVEL " + PlayerPrefs.GetInt("StartShipLevel").ToString();

        Debug.Log(myListeners, gameObject);



        ShipAudio.enabled = true;
        Time.timeScale = 1;
        startLevel = PlayerPrefs.GetInt("StartShipLevel", 1);
        toggleCam = false;
        once = true;
    }


    public void GamePaused()
    {
        ShipAudio.enabled = false;


        AdmobManager.instance.ShowBanner(AdmobManager.BannerAD.SmallBanner);
        AdmobManager.instance.ShowInterstitialAd();
        BoatController.mute = true;
        Time.timeScale = 0;
        if (once)
        {
            //	if(HZVideoAd.isAvailable())
            {
                //		HZVideoAd.show();
                //		once = false;
            }
        }
        gamePause.SetActive(true);
        levelComplete.SetActive(false);
        gameOverDrop.SetActive(false);
        //AdmobManager.instance.adsCanvas.SetActive(true);
        AdmobManager.instance.ShowBanner(AdmobManager.BannerAD.SmallBanner);

        rccCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void LevelComplete()
    {

        BoatController.mute = true;
        rccCanvas.GetComponent<Canvas>().enabled = false;
        GameObject.Find("GameManager").GetComponent<AudioSource>().clip = gameWin;
        GameObject.Find("GameManager").GetComponent<AudioSource>().Play();
        if (Health.healthSliderValue > 0.8)
        {
            scorePoints = 1000;
            score.text = scorePoints.ToString();
            StartCoroutine(ShowStars(3));
        }
        else if (Health.healthSliderValue > 0.5)
        {
            scorePoints = 500;
            score.text = scorePoints.ToString();
            StartCoroutine(ShowStars(2));
        }
        else if (Health.healthSliderValue >= 0.2)
        {
            scorePoints = 200;
            score.text = scorePoints.ToString();
            StartCoroutine(ShowStars(1));
        }




        StartCoroutine(LevelCompleteWait(GameObject.Find("GameManager").GetComponent<AudioSource>().clip.length));
    }

    IEnumerator ShowStars(int count)
    {
        starsCount = count;
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(1f);
            stars[i].SetActive(true);
        }

    }

    IEnumerator LevelCompleteWait(float time)
    {
        yield return new WaitForSeconds(time);
        Time.timeScale = 1;
        if (once)
        {
            //	if(HZVideoAd.isAvailable())
            {
                //	HZVideoAd.show();
                once = false;
            }
        }
        AdmobManager.instance.ShowInterstitialAd();
        Debug.Log(Health.healthSliderValue);
        levelComplete.SetActive(true);
        AdmobManager.instance.adsCanvas.SetActive(true);
        AdmobManager.instance.ShowBanner(AdmobManager.BannerAD.SmallBanner);
        gamePause.SetActive(false);
        gameOverDrop.SetActive(false);


    }

    public void GameOverCrashed()
    {
        AdmobManager.instance.ShowBanner(AdmobManager.BannerAD.SmallBanner);
        BoatController.mute = true;
        GameObject.Find("GameManager").GetComponent<AudioSource>().clip = burning;
        GameObject.Find("GameManager").GetComponent<AudioSource>().Play();
        StartCoroutine(GameOverCrashedWait(6.0f));
        rccCanvas.GetComponent<Canvas>().enabled = false;

    }

    IEnumerator GameOverCrashedWait(float time)
    {

        yield return new WaitForSeconds(2.5f);
        ShipCrashed.SetActive(true);
        yield return new WaitForSeconds(time);
        levelComplete.SetActive(false);
        gamePause.SetActive(false);
        ShipCrashed.SetActive(false);
        gameOverDrop.SetActive(true);
        AdmobManager.instance.adsCanvas.SetActive(true);
        AdmobManager.instance.ShowBanner(AdmobManager.BannerAD.SmallBanner);
        Time.timeScale = 1;
        if (once)
        {
            //	if(HZInterstitialAd.isAvailable())
            {
                //		HZInterstitialAd.show();
                once = false;
            }
        }

    }

    public void GameOverDrop()
    {
        AdmobManager.instance.ShowBanner(AdmobManager.BannerAD.SmallBanner);
        StartCoroutine(GameOverWait(4.0f));
        rccCanvas.GetComponent<Canvas>().enabled = false;

    }

    IEnumerator GameOverWait(float time)
    {
        yield return new WaitForSeconds(2.0f);
        LuggageLost.SetActive(true);
        yield return new WaitForSeconds(time);
        BoatController.mute = true;
        GameObject.Find("GameManager").GetComponent<AudioSource>().clip = gameOver;
        GameObject.Find("GameManager").GetComponent<AudioSource>().Play();
        levelComplete.SetActive(false);
        gamePause.SetActive(false);
        LuggageLost.SetActive(false);
        gameOverDrop.SetActive(true);
        AdmobManager.instance.adsCanvas.SetActive(true);
        AdmobManager.instance.ShowBanner(AdmobManager.BannerAD.SmallBanner);
        Time.timeScale = 1;
        if (once)
        {
            
            {
               
                once = false;
            }
        }

    }

    public void Resume()
    {
        AdmobManager.instance.ShowBanner(AdmobManager.BannerAD.SmallBanner);
        ShipAudio.enabled = true;
        Time.timeScale = 1;
        BoatController.mute = false;
        rccCanvas.GetComponent<Canvas>().enabled = true;
        gamePause.SetActive(false);
    }

    public void LevelSelection()
    {
        if (Time.timeScale < 1) Time.timeScale = 1;
        levelSelection = true;
        SceneLoader.instance.LoadNextScene(Scenes.MainMenu);

    }

    public void Replay()
    {
        if (Time.timeScale < 1) Time.timeScale = 1;
        AdmobManager.instance.ShowInterstitialAd();

        SceneLoader.instance.LoadNextScene(Scenes.Gameplay);
    }

    public void NextLevel()
    {
        AdmobManager.instance.ShowBanner(AdmobManager.BannerAD.SmallBanner);
        PlayerPrefs.SetInt("StartShipLevel", startLevel + 1);
        SceneLoader.instance.LoadNextScene(Scenes.Gameplay);
    }

    public void MainMenu()
    {
        if (Time.timeScale < 1) Time.timeScale = 1;
        AdmobManager.instance.ShowInterstitialAd();
        SceneLoader.instance.LoadNextScene(Scenes.MainMenu);

    }

    public void changeCamera()
    {
        toggleCam = !toggleCam;
        if (toggleCam)
        {
            rccMainCam.SetActive(false);
            vanInsideCam.SetActive(true);
        }
        else
        {
            rccMainCam.SetActive(true);
            vanInsideCam.SetActive(false);
        }
    }
}
