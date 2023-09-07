
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static AdsManager instance;
    [SerializeField] string androidGameId;
    [SerializeField] bool testMode = true;
    [SerializeField] string interstitialAdUnitId = "Android_Interstitial";
    [SerializeField] string rewardedAdUnitId = "Android_Interstitial";
    [SerializeField] string bannerAdUnitId = "Banner";
    [SerializeField] BannerPosition bannerPosition;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private IEnumerator Start()
    {

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            yield return new WaitForSecondsRealtime(2f);
            Debug.Log("Network Not Reachable");
            
         //   SceneLoader.instance.LoadNextScene(SceneLoader.Scenes.MainMenu);
        }
        else
        {
            yield return new WaitForSecondsRealtime(2f);
            InitializeAds();
        }
    }



    public void InitializeAds()
    {
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(androidGameId, testMode, this);
        }
    }
    public void OnInitializationComplete()
    {
        LoadIntersitialAd();
      //  LoadRewardedAd();
        LoadBanner();

      //  SceneLoader.instance.LoadNextScene(SceneLoader.Scenes.MainMenu);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
     //   SceneLoader.instance.LoadNextScene(SceneLoader.Scenes.MainMenu);
    }

    public void LoadRewardedAd()
    {

        Advertisement.Load(rewardedAdUnitId, this);
    }
    public void ShowRewarded()
    {
        Advertisement.Show(rewardedAdUnitId, this);
    }
    #region Unity Intersitial
    public void LoadIntersitialAd()
    {

        Advertisement.Load(interstitialAdUnitId, this);
    }



    public void ShowInterstitial()
    {

        Advertisement.Show(interstitialAdUnitId, this);
    }


    public void OnUnityAdsAdLoaded(string adUnitId)
    {

    }

    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }

    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
    }

    public void OnUnityAdsShowStart(string _adUnitId) { }
    public void OnUnityAdsShowClick(string _adUnitId) { }
    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (rewardedAdUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            //if (MainMenuUI.instance != null) MainMenuUI.instance.UnlockCar();
            //LoadRewardedAd();
            return;
        }
        LoadIntersitialAd();
    }
    #endregion


    public void LoadBanner()
    {
        // Set up options to notify the SDK of load events:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        // Load the Ad Unit with banner content:
        Advertisement.Banner.Load(bannerAdUnitId, options);
    }

    // Implement code to execute when the loadCallback event triggers:
    void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");

    }

    // Implement code to execute when the load errorCallback event triggers:
    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
        // Optionally execute additional code, such as attempting to load another ad.
    }

    // Implement a method to call when the Show Banner button is clicked:
    public void ShowBannerAd()
    {
        // Set up options to notify the SDK of show events:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        // Show the loaded Banner Ad Unit:
        Advertisement.Banner.SetPosition(bannerPosition);
        Advertisement.Banner.Show(bannerAdUnitId, options);
    }

    // Implement a method to call when the Hide Banner button is clicked:
    void HideBannerAd()
    {
        // Hide the banner:
        Advertisement.Banner.Hide();
    }

    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }

}
