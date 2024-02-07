namespace Ilumisoft.BubblePop
{
    using System.Collections;

    public class UpdateRevenuePreview : ICoroutine
    {
        GameUI gameUI;

        Selection selection;

        public UpdateRevenuePreview(GameUIManager gameUIManager, Selection selection)
        {
            gameUI = gameUIManager.GameUI;

            this.selection = selection;
        }

        public IEnumerator Execute()
        {
            ScoreRevenue scoreRevenue = new ScoreRevenue(selection);

            if (scoreRevenue.Value > 0)
            {
                gameUI.ShowScorePreview(scoreRevenue.Value);
            }
            else
            {
                gameUI.HideScorePreview();
            }

            yield return null;
        }
    }
}