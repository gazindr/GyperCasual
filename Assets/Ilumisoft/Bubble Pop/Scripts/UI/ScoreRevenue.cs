namespace Ilumisoft.BubblePop
{
    public struct ScoreRevenue
    {
        Selection selection;

        public ScoreRevenue(Selection selection)
        {
            this.selection = selection;
        }

        public int Value
        {
            get
            {
                int count = selection.Selected.Count;

                return count * (count - 1);
            }
        }
    }
}