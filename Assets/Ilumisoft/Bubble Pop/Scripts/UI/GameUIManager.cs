namespace Ilumisoft.BubblePop
{
    using System.Collections;
    using InstantGamesBridge.Modules.Game;
    using UnityEngine;

    public class GameUIManager : MonoBehaviour
    {
        [SerializeField]
        GameUI gameUI = null;

        [SerializeField]
        GameOverUI gameOverUI = null;

        [SerializeField]
        GameObject lastChanceUI = null;

        [SerializeField]
        OverlayCanvas overlayCanvas = null;

        public GameMode gameMode;

       

        public GameUI GameUI => gameUI;

        public void ShowGameOverUI()
        {
            StartCoroutine(ShowGameOverUICoroutine());
        }

        public void ShowLastChanceUI()
        {
            StartCoroutine(ShowLastChanceCoroutine());
        }

        public void HideLastChanceUI()
        {
            StartCoroutine(BackToGameCoroutine());
        }

        IEnumerator ShowLastChanceCoroutine()
        {
            yield return overlayCanvas.FadeIn();
            gameUI.gameObject.SetActive(false);
            lastChanceUI.SetActive(true);
            MovesManager.Instance.UpdateButton();
            yield return overlayCanvas.FadeOut();

        }
        IEnumerator BackToGameCoroutine()
        {
            yield return overlayCanvas.FadeIn();
            gameUI.gameObject.SetActive(true);
            lastChanceUI.SetActive(false);
            MovesManager.Instance.UpdateButton();
            yield return gameMode.RunGame();
            yield return overlayCanvas.FadeOut();
        }

        IEnumerator ShowGameOverUICoroutine()
        {
            yield return overlayCanvas.FadeIn();
            gameUI.gameObject.SetActive(false);
            lastChanceUI.SetActive(false);
            gameOverUI.gameObject.SetActive(true);
            yield return overlayCanvas.FadeOut();
        }
    }
}