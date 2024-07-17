using InstantGamesBridge;
using InstantGamesBridge.Modules.Advertisement;
using InstantGamesBridge.Modules.Game;
using InstantGamesBridge.Modules.Platform;
using System.Collections;
using UnityEngine;

public partial class AdsManager : MonoBehaviour
{
#if !UNITY_EDITOR
     public int INT_DEALY = 180;
#else
     public int INT_DEALY = 5;
#endif
    public static AdsManager Instance;
    [SerializeField] AdDelayScreen adDelayScreen;
    [SerializeField] int callGRADelay = -1;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    IEnumerator Start()
    {
        yield return null;
        InitializeAdDependecies();
        StartCoroutine(DelayGRA());
    }
    IEnumerator DelayGRA()
    {
        if (callGRADelay < 0)
            yield break;
        if (callGRADelay > 0)
        {
            yield return new WaitForSeconds(callGRADelay);
        }
        Bridge.platform.SendMessage(PlatformMessage.GameReady);

    }
    public void UpdateInterValue(int val)
    {
        INT_DEALY = val;
        Bridge.advertisement.SetMinimumDelayBetweenInterstitial(INT_DEALY);
    }
    void InitializeAdDependecies()
    {
        /*if (Bridge.player.isAuthorizationSupported)
        {
            if (Bridge.platform.id == InstantGamesBridge.Common.PlatformId.VK)
                Bridge.player.Authorize();
        }*/

        Bridge.game.visibilityStateChanged += OnGameVisibilityStateChanged;

        Bridge.advertisement.interstitialStateChanged += OnInterstitialStateChanged;
        Bridge.advertisement.rewardedStateChanged += OnRewardedStateChanged;

        Bridge.advertisement.SetMinimumDelayBetweenInterstitial(INT_DEALY);

        /*if(adDelayScreen!=null)
        {
            if (Bridge.platform.id == InstantGamesBridge.Common.PlatformId.VK)
            {
                Destroy(adDelayScreen.gameObject);
            }
        }*/
    }
    public void ShowBanner()
    {
        if (Bridge.advertisement.isBannerSupported
            && Bridge.platform.id == InstantGamesBridge.Common.PlatformId.VK)
            Bridge.advertisement.ShowBanner();
    }
    public void HideBanner()
    {
        if (Bridge.advertisement.isBannerSupported
            && Bridge.platform.id == InstantGamesBridge.Common.PlatformId.VK)
            Bridge.advertisement.HideBanner();
    }

    private void OnGameVisibilityStateChanged(VisibilityState state)
    {
        switch (state)
        {
            case VisibilityState.Visible:
                VisChangeVis();
                break;
            case VisibilityState.Hidden:
                VisChangeHid();
                break;
        }
    }
    void VisChangeVis()
    {
        UnPauseForAd();
        //GlobalVolumeManager.UnMuteSoundFocus();
    }
    void VisChangeHid()
    {
        PauseForAd();
        //GlobalVolumeManager.MuteSoundFocus();
    }

    public float lastPingTime = 0;
    public void PingPopAd()
    {
        if (Time.unscaledTime - lastRVTime < 10)
        {
            Debug.Log("Rewarded ad was just recently");
            return;
        }
        if (Time.unscaledTime - lastPingTime > INT_DEALY)
        {
            if(adDelayScreen!=null)
            {
                Debug.Log("ShowAd with delay");
                //SettingsMenu.Instance.Pause();
                timePingDelay = Time.unscaledTime;
                lastPingTime = Time.unscaledTime;
                adDelayScreen.ShowDelay();
                return;
            }
            ShowInterstitial();
            lastPingTime = Time.unscaledTime;
        }
        else
        {
            //RateGameManager.RequestRating();
            Debug.Log("Next ad ping in:" + (INT_DEALY - (int)(Time.unscaledTime - lastPingTime)));
        }
    }
    float lastRVTime = 0;
    
    public void ShowInterstitial()
    {
        Debug.Log("Called ShowInterstitial");
        Bridge.advertisement.ShowInterstitial();
#if UNITY_EDITOR
        UnPauseForAd();
#endif
    }
    public void ShowRewardedAd(RewardType adType)
    {
        lastRVTime = Time.unscaledTime;
        activeRewardType = adType;
        ShowReward();
    }
    void ShowReward()
    {
        Debug.Log("Called ShowRewardedAd");
        Bridge.advertisement.ShowRewarded();
        lastRVTime = Time.unscaledTime;
#if UNITY_EDITOR
        UnPauseForAd();
#endif
    }

    private void OnInterstitialStateChanged(InterstitialState state)
    {
        Debug.Log("OnInterstitialStateChanged" + state);
        switch (state)
        {
            case InterstitialState.Loading:
                break;
            case InterstitialState.Failed:
                lastPingTime = Time.unscaledTime - INT_DEALY/2f;
                UnPauseForAd();
                break;
            case InterstitialState.Opened:
                lastPingTime = Time.unscaledTime;
                PauseForAd();
                break;
            case InterstitialState.Closed:
                UnPauseForAd();
                break;
        }

    }
    private void OnRewardedStateChanged(RewardedState state)
    {
        Debug.Log("OnRewardedStateChanged" + state);
        switch (state)
        {
            case RewardedState.Loading:
                break;
            case RewardedState.Failed:
                UnPauseForAd();
                break;
            case RewardedState.Opened:
                PauseForAd();
                break;
            case RewardedState.Closed:
                UnPauseForAd();
                break;
            case RewardedState.Rewarded:
                RewardUser();
                break;
        }
    }

    private static float timeScaleWas = 1f;
    private static bool isInAd = false;
    protected float timePingDelay = 0f;
    public static bool IsInAd()
    {
        if (Instance == null) return false;

        if (isInAd) return true;
        if (Time.unscaledTime - Instance.timePingDelay < 3f) return true;
        if (AdDelayScreen.IsInCountDown) return true;

        return false;
    }
    public static void PauseForAd()
    {
        if (isInAd) return;
        isInAd = true;
        GlobalVolumeManager.MuteSoundAd();
        timeScaleWas = Time.timeScale;

        //PauseMenu.Instance.ForceEnterPause();

        Time.timeScale = 0f;
        AudioListener.volume = 0f;
        Debug.Log("PauseForAd ts:" + timeScaleWas);
    }
    public static void UnPauseForAd()
    {
        if (!isInAd) return;
        isInAd = false;

        GlobalVolumeManager.UnMuteSoundAd();
        Time.timeScale = timeScaleWas;
        Debug.Log("UnPauseForAd ts:" + timeScaleWas);
    }
   
}