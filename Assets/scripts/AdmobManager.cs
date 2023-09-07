// using GameAnalyticsSDK;
using GoogleMobileAds.Api;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdmobManager : MonoBehaviour
{
    public enum BannerAD
    {
        SmallBanner,
        LargeBanner,
        Both
    }
    public static bool isShowingAd;
    public GameObject adsCanvas;
    public bool canShowAppOpen;
    [SerializeField] private bool testMode;
    public static bool appOpenfirst;
    #region Singleton
    public static AdmobManager instance;
    private void Awake()
    {
      //  GameAnalytics.Initialize();
        if (instance != null)
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

    //==========================================Varaibles===============================================//
    #region AppOpen Variables
    private string appOpenId = "ca-app-pub-5585447852902267/9383246671";
    private DateTime _expireTime;
    private AppOpenAd appOpenAd;
    public bool IsAdAvailable
    {
        get
        {
            return appOpenAd != null
                   && DateTime.Now < _expireTime;
        }
    }

    #endregion

    #region Banner Variables
    private string smallBannerId = "ca-app-pub-5585447852902267/2013675506";
    [HideInInspector] public BannerAD bannerSize;
    private BannerView smallBannerView;
    private BannerView largeBannerView;
    #endregion

    #region Interstitial Variables
    private string interAdId = "ca-app-pub-5585447852902267/2107391885";
    private InterstitialAd interstitialAd;
    #endregion

    private string rewardedAdId = "ca-app-pub-5585447852902267/4809535288";
    private RewardedAd rewardedAd;
    void Start()
    {

        if (testMode)
        {
            appOpenId = "ca-app-pub-3940256099942544/3419835294";
            smallBannerId = "ca-app-pub-3940256099942544/6300978111";
            interAdId = "ca-app-pub-3940256099942544/1033173712";
            rewardedAdId = "ca-app-pub-3940256099942544/5224354917";
        }

        appOpenfirst = false;
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        MobileAds.Initialize((initStatus) =>
        {
            Dictionary<string, AdapterStatus> map = initStatus.getAdapterStatusMap();
            foreach (KeyValuePair<string, AdapterStatus> keyValuePair in map)
            {
                string className = keyValuePair.Key;
                AdapterStatus status = keyValuePair.Value;
                switch (status.InitializationState)
                {
                    case AdapterState.NotReady:
                        // The adapter initialization did not complete.
                        print("Adapter: " + className + " not ready.");
                        break;
                    case AdapterState.Ready:
                        // The adapter was successfully initialized.
                        print("Adapter: " + className + " is initialized.");
                        break;
                }

            }
        });
        LoadAppOpenAd();
        LoadBannerAd();
        LoadInterstitialAd();
      //   AdsManager.InitializeAds();
        // AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
        Invoke(nameof(GoToMainMenu), 4f);
    }

   private void GoToMainMenu()
    {
        SceneLoader.instance.LoadNextScene(Scenes.MainMenu);
    }


    #region AppOpen


    public void LoadAppOpenAd()
    {
        if (!appOpenfirst)
            isShowingAd = true;
        else
        {
            isShowingAd = false;
        }
        // Clean up the old ad before loading a new one.
        if (appOpenAd != null)
        {
            appOpenAd.Destroy();
            appOpenAd = null;
        }

        Debug.Log("Loading the app open ad.");

        // Create our request used to load the ad.
        var adRequest = new AdRequest.Builder().Build();

        // send the request to load the ad.
        AppOpenAd.Load(appOpenId, ScreenOrientation.LandscapeLeft, adRequest,
        (AppOpenAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("app open ad failed to load an ad " +
                                   "with error : " + error);
                    isShowingAd = false;
                    appOpenfirst = true;
                    return;
                }

                Debug.Log("App open ad loaded with response : "
                          + ad.GetResponseInfo());
                // App open ads can be preloaded for up to 4 hours.
                _expireTime = DateTime.Now + TimeSpan.FromHours(4);



                appOpenAd = ad;
                RegisterEventHandlers(ad);
                if (!appOpenfirst)
                {
                    ShowAppOpenAd();
                    

                }

                   
            });
    }
    private void RegisterEventHandlers(AppOpenAd ad)
    {

        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("App open ad full screen content opened.");
           /* if (smallBannerView != null)
                HideBanner(BannerAD.SmallBanner);
            */

        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;

            // Check if the current scene's build index is 1
            if (currentBuildIndex == 1)
            {
                ShowBanner(BannerAD.SmallBanner);
            }
                appOpenfirst = true;
            isShowingAd = false;
            LoadAppOpenAd();
           
            
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            
            appOpenfirst = true;
            isShowingAd = false;
            Debug.LogError("App open ad failed to open full screen content " +
                           "with error : " + error);
            LoadAppOpenAd();
        };
    }

    public void ShowAppOpenAd()
    {
        Debug.Log("App open ad full screen content opened.");
        if (smallBannerView != null)
            HideBanner(BannerAD.SmallBanner);
        if (appOpenAd != null && appOpenAd.CanShowAd())
        {
            Debug.Log("Showing app open ad.");
            appOpenAd.Show();
        }
        else
        {
            Debug.LogError("App open ad is not ready yet.");
            //CrossPromotion.Instance.ShowCrossPromotion();
        }
    }

    private void OnApplicationFocus(bool focus)
    {

        if (focus && canShowAppOpen && IsAdAvailable)
            ShowAppOpenAd();
    }   

    #endregion
    //Optimization Remaining
    #region Banner Functions
    public void LoadBannerAd()
    {
        // create an instance of a banner view first.
        if (smallBannerView == null)
        {
            CreateBannerView();
        }
        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder().Build();
        var adRequestLarge = new AdRequest.Builder().Build();

        // send the request to load the ad.
        Debug.Log("Loading banner ad.");
        smallBannerView.LoadAd(adRequest);
        largeBannerView.LoadAd(adRequestLarge);
        HideBanner(BannerAD.Both);
    }
    public void CreateBannerView()
    {
        Debug.Log("Creating banner view");


        if (smallBannerView != null)
        {
            DestroyAd(smallBannerView);
        }
        if (largeBannerView != null)
        {
            DestroyAd(largeBannerView);
        }
        smallBannerView = new BannerView(smallBannerId, AdSize.Banner, AdPosition.Top);
        largeBannerView = new BannerView(smallBannerId, AdSize.MediumRectangle, AdPosition.BottomRight);
        smallBannerView.OnBannerAdLoaded += OnBannerAdLoaded;
        smallBannerView.OnBannerAdLoadFailed += OnBannerAdLoadFailed;
        largeBannerView.OnBannerAdLoaded += OnBannerAdLoaded;
        largeBannerView.OnBannerAdLoadFailed += OnBannerAdLoadFailed;
        largeBannerView.OnAdPaid += OnBannerAdPaid;
        smallBannerView.OnAdPaid += OnBannerAdPaid;
    }

    private void OnBannerAdPaid(AdValue obj)
    {

    }

    private void OnBannerAdLoaded()
    {
        Debug.Log("Banner Ad Loaded");
    }

    private void OnBannerAdLoadFailed(LoadAdError obj)
    {

        Debug.Log("Banner Ad not Loaded");
    }



    public void ShowBanner(BannerAD banner)
    {
        adsCanvas.SetActive(true);
        switch (banner)
        {
            case BannerAD.SmallBanner:
                smallBannerView.Show();
                break;
            case BannerAD.LargeBanner:
                largeBannerView.Show();
                break;
            case BannerAD.Both:
                smallBannerView.Show();
                largeBannerView.Show();
                break;

        }
        adsCanvas.SetActive(true);
    }
    public void HideBanner(BannerAD banner)
    {
        switch (banner)
        {
            case BannerAD.SmallBanner:
                smallBannerView.Hide();
                break;
            case BannerAD.LargeBanner:
                largeBannerView.Hide();
                break;
            case BannerAD.Both:
                smallBannerView.Hide();
                largeBannerView.Hide();
                break;

        }
        adsCanvas.SetActive(false);
    }

    public void DestroyAd(BannerView view)
    {
        if (view != null)
        {
            Debug.Log("Destroying banner ad.");
            view.Destroy();
            view = null;
        }
    }

    #endregion

    #region Interstitial Functions

    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder().Build();

        // send the request to load the ad.
        InterstitialAd.Load(interAdId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitialAd = ad;

                RegisterReloadHandler(interstitialAd);
                canShowAppOpen = true;

            });
    }
    public void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            interstitialAd.Show();
            canShowAppOpen = false;
            isShowingAd = true;
        }
        else
        {
        //    Debug.LogError("Interstitial ad is not ready yet.");
         //   CrossPromotion.Instance.ShowCrossPromotion();
            LoadInterstitialAd();
        }
    }

    private void RegisterReloadHandler(InterstitialAd ad)
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
    {
        Debug.Log("Interstitial Ad full screen content closed.");
        isShowingAd = false;
        // Reload the ad so that we can show another as soon as possible.
        LoadInterstitialAd();
    };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
            isShowingAd = false;
            // Reload the ad so that we can show another as soon as possible.
            LoadInterstitialAd();
        };
        ad.OnAdPaid += (AdValue adValue) =>
        {
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
    }
    #endregion

    #region Rewarded Functions
    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder().Build();

        // send the request to load the ad.
        RewardedAd.Load(rewardedAdId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                //    MainMenuUI.instance.RewardFailed();
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;
                RegisterEventHandlers(rewardedAd);
                isShowingAd = false;

                ShowRewardedAd();
            });
    }

    public void ShowRewardedAd()
    {
        isShowingAd = true;
        canShowAppOpen = false;
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {/*
                // TODO: Reward the user.
                if (MainMenuUI.instance != null) MainMenuUI.instance.UnlockCar();


                switch (rewardType)
                {
                    case RewardType.Car:
                        break;
                    case RewardType.Cash:

                        MainMenuUI.instance.OnRewardGiven(rewardType);
                        break;

                }*/
            });
        }
    }
    private void RegisterEventHandlers(RewardedAd ad)
    {

        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");

        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
        };
        ad.OnAdPaid += (AdValue adValue) =>
        {
        };
    }
    #endregion
}
