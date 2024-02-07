namespace Ilumisoft.BubblePop
{
    using System.Collections.Generic;

    public class Selection
    {
        public List<Bubble> Selected { get; } = new List<Bubble>();

        public bool IsEmpty => Selected.Count == 0;

        public void Clear()
        {
            Selected.Clear();
        }

        public bool Contains(Bubble bubble)
        {
            return Selected.Contains(bubble);
        }
    }
}