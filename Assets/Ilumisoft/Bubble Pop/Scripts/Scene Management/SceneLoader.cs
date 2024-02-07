namespace Ilumisoft.BubblePop
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class SceneLoader : MonoBehaviour
    {
        [SerializeField]
        OverlayCanvas overlayCanvas = null;

        IEnumerator Start()
        {
            yield return overlayCanvas.FadeOut();
        }

        public void LoadScene(string name)
        {
            
            StopAllCoroutines();
            if (Time.unscaledTime - AdsManager.Instance.lastPingTime > 90)
            {
                AdsManager.Instance.PingPopAd();
                //SceneManager.LoadScene(name);
                StartCoroutine(WaitForRestart(name));
            } else
            {
                StartCoroutine(LoadSceneCoroutine(name));

            }           
            
            
        }
        IEnumerator WaitForRestart(string name)
        {
            yield return new WaitForSecondsRealtime(3f);
            StartCoroutine(LoadSceneCoroutine(name));
        }
        IEnumerator LoadSceneCoroutine(string name)
        {
            yield return overlayCanvas.FadeIn();

            SceneManager.LoadScene(name);
        }
    }
}