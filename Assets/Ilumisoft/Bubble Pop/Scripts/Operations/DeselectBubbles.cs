namespace Ilumisoft.BubblePop
{
    using System.Collections;

    public class DeselectBubbles : ICoroutine
    {
        Selection selection;

        public DeselectBubbles(Selection selection)
        {
            this.selection = selection;
        }

        public IEnumerator Execute()
        {
            if(selection.IsEmpty)
            {
                yield break;
            }

            foreach (var bubble in selection.Selected)
            {
                bubble.PlayDeselectAnimation();
            }

            selection.Clear();
        }
    }
}