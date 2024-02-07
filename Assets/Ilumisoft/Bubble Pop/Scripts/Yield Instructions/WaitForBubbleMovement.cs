namespace Ilumisoft.BubblePop
{
    using UnityEngine;

    /// <summary>
    /// Waits until no bubble in the grid is moving anymore
    /// </summary>
    public class WaitForBubbleMovement : CustomYieldInstruction
    {
        BubbleGrid grid;

        public WaitForBubbleMovement(BubbleGrid grid)
        {
            this.grid = grid;
        }

        public override bool keepWaiting
        {
            get
            {
                return grid.IsAnyBubbleMoving();
            }
        }
    }
}