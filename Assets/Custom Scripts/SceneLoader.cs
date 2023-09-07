using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    #region Singleton
    public static SceneLoader instance;
    private void Awake()
    {
        if (instance == this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    private void Start()
    {
        //LoadNextScene(Scenes.MainMenu);
    }
    public GameObject loading;
    public Scenes currentScene;
    public Scenes lastScene;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += TurnLoadingOff;
    }

    private void TurnLoadingOff(Scene arg0, LoadSceneMode arg1)
    {
        loading.SetActive(false);
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= TurnLoadingOff;
    }

    public void LoadNextScene(Scenes scene)
    {
        AdmobManager.instance.ShowBanner(AdmobManager.BannerAD.SmallBanner);
        if (currentScene == Scenes.Splash)
        {
            loading.SetActive(false);
        }
        else
        {
            loading.SetActive(true);

        }
        lastScene = currentScene;
        currentScene = scene;

        StartCoroutine(Loading(scene));

    }

    IEnumerator Loading(Scenes scene)
    {

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(scene.GetHashCode());

    }

}
public enum Scenes
{
    Splash,
    MainMenu,
    Gameplay,
    
}