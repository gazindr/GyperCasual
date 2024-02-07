using System.Collections;
using UnityEngine;
using TMPro;

public class AdDelayScreen : MonoBehaviour
{
    [SerializeField] TMP_Text countDownT;
    [SerializeField] Canvas _canvas;
    private void Awake()
    {
        _canvas.enabled = false;
        if(InstantGamesBridge.Bridge.platform.id == InstantGamesBridge.Common.PlatformId.VK)
        {
            Destroy(gameObject);
        }
    }
    public void ShowDelay()
    {
        if (countDown != null) return;

        if (countDown != null)
            StopCoroutine(countDown);
        countDown = StartCoroutine(CountDown());
    }
    public static bool IsInCountDown = false;
    Coroutine countDown;
    IEnumerator CountDown()
    {
        IsInCountDown = true;
        _canvas.enabled = true;
        countDownT.text = "3";
        yield return new WaitForSecondsRealtime(0.7f);
        countDownT.text = "2";
        yield return new WaitForSecondsRealtime(0.7f);
        countDownT.text = "1";
        yield return new WaitForSecondsRealtime(0.7f);
        _canvas.enabled = false;
        IsInCountDown = false;
        AdsManager.Instance.ShowAd(AdsManager.AdType.Interstitial);
        countDown = null;

    }
}
