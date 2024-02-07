namespace Ilumisoft.BubblePop
{
    using System.Collections;

    public class HideRevenuePreview : ICoroutine
    {
        GameUI gameUI;

        public HideRevenuePreview(GameUIManager gameUIManager)
        {
            gameUI = gameUIManager.GameUI;
        }

        public IEnumerator Execute()
        {
            gameUI.HideScorePreview();

            yield return null;
        }
    }
}