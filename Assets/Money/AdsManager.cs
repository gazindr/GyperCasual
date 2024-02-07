using InstantGamesBridge;
using InstantGamesBridge.Modules.Advertisement;
using InstantGamesBridge.Modules.Game;
using InstantGamesBridge.Modules.Platform;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
#if !UNITY_EDITOR
    public const int INT_DEALY = 90;
#else
    public const int INT_DEALY = 10;
#endif
    public static AdsManager Instance;
    AdType activeAdType = AdType.Interstitial;
    [SerializeField] AdDelayScreen adDelayScreen;
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

        /*if (Bridge.player.isAuthorizationSupported)
        {
            if (Bridge.platform.id == InstantGamesBridge.Common.PlatformId.VK)
                Bridge.player.Authorize();
        }*/

        Bridge.platform.SendMessage(PlatformMessage.GameReady);

        Bridge.game.visibilityStateChanged += OnGameVisibilityStateChanged;


        Bridge.advertisement.interstitialStateChanged += OnInterstitialStateChanged;
        Bridge.advertisement.rewardedStateChanged += OnRewardedStateChanged;

        Bridge.advertisement.SetMinimumDelayBetweenInterstitial(INT_DEALY);
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
        GlobalVolumeManager.UnMuteSoundFocus();
    }
    void VisChangeHid()
    {
        GlobalVolumeManager.MuteSoundFocus();
    }

    public float lastPingTime = 0;
    public void PingPopAd()
    {
        if (Time.unscaledTime - lastRVTime < 10)
        {
            Debug.Log("Rewarded ad was jsut recently");
            return;
        }
        if (Time.unscaledTime - lastPingTime > INT_DEALY)
        {
            if(adDelayScreen!=null)
            {
                Debug.Log("ShowAd with delay");
                timePingDelay = Time.unscaledTime;
                adDelayScreen.ShowDelay();
                return;
            }
            if (ShowAd(AdType.Interstitial))
            {
                Debug.Log("Ping pop ad: ShowAd");
                lastPingTime = Time.unscaledTime;
            }

        }
        else
        {
            Debug.Log("Next ad ping in:" + (INT_DEALY - (int)(Time.unscaledTime - lastPingTime)));
        }
    }
    public bool isAdReady(AdType adType)
    {
        if (adType == AdType.Interstitial)
        {
            return (Time.unscaledTime - lastPingTime > INT_DEALY);
        }
        else
        {
            return true;
        }
    }
    float lastRVTime = 0;
    public void RewardUser()
    {
        lastRVTime = Time.unscaledTime;
        Debug.LogError("RewardUser:"+ activeAdType.ToString());
        if (activeAdType == AdType.RewardedMap)
        {
            
                Debug.LogError("LevelMenu.Instance == null");
            
        }
    }
    public bool ShowAd(AdType adType)
    {
        if (adType == AdType.Interstitial)
        {
            if (isAdReady(adType))
            {
                ShowInterstitial();
                return true;
            }
            return false;

        }
        else
        {
            lastRVTime = Time.unscaledTime;
            activeAdType = adType;
            ShowReward();
            return true;
        }
    }
    void ShowInterstitial()
    {
        Debug.Log("Called ShowInterstitial");
        Bridge.advertisement.ShowInterstitial();
        //YandexSDK.YaSDK.instance.ShowInterstitial();
    }
    void ShowReward()
    {
        Debug.Log("Called ShowReward");
        Bridge.advertisement.ShowRewarded();
        //YandexSDK.YaSDK.instance.ShowRewarded("rv_klg");
    }
    private void OnInterstitialStateChanged(InterstitialState state)
    {
        Debug.Log("OnInterstitialStateChanged" + state);
        switch (state)
        {
            case InterstitialState.Loading:
                break;
            case InterstitialState.Failed:
                isInAd = false;
                break;
            case InterstitialState.Opened:
                lastPingTime = Time.unscaledTime;
                AdStarted();
                break;
            case InterstitialState.Closed:
                AdClosed();
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
                isInAd = false;
                break;
            case RewardedState.Opened:
                AdStarted();
                break;
            case RewardedState.Closed:
                AdClosed();
                break;
            case RewardedState.Rewarded:
                RewardUser();
                break;
        }
    }

    float timeScaleWas = 1f;
    bool isInAd = false;
    float timePingDelay = 0f;
    public bool IsInAd()
    {
        return isInAd || (Time.unscaledTime- timePingDelay < 2.5f);
    }
    //EventSystem disabledEventSys = null;
    void AdStarted()
    {
        if (isInAd) return;
        isInAd = true;
        GlobalVolumeManager.MuteSoundAd();
        timeScaleWas = Time.timeScale;
        Time.timeScale = 0f;
        AudioListener.volume = 0f;
        /*if (EventSystem.current != null)
        {
            disabledEventSys = EventSystem.current;
            EventSystem.current.enabled = false;
        }*/
        Debug.Log("AdStarted TimeScaleWas:" + timeScaleWas);
    }
    void AdClosed()
    {
        if (!isInAd) return;
        isInAd = false;

        /*if(EventSystem.current!=null) EventSystem.current.enabled = true;
        if(disabledEventSys != null) disabledEventSys.enabled = true;*/
        GlobalVolumeManager.UnMuteSoundAd();
        Time.timeScale = timeScaleWas;
        Debug.Log("AdClosed TimeScaleWas:" + timeScaleWas);
    }
    public enum AdType
    {
        Interstitial,
        RewardedMap

    }
}