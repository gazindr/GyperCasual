namespace Ilumisoft.BubblePop
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ProcessHorizontalMovement : ICoroutine
    {
        BubbleGrid grid;

        public ProcessHorizontalMovement(BubbleGrid grid)
        {
            this.grid = grid;
        }

        public IEnumerator Execute()
        {
            BudgeColumnsUp();

            yield return new WaitForBubbleMovement(grid);
        }

        /// <summary>
        /// Makes all columns move to the left, to fill empty space between columns
        /// </summary>
        private void BudgeColumnsUp()
        {
            for (int x = 0; x < grid.Width; x++)
            {
                if (grid.IsColumnEmpty(x))
                {
                    BudgeNextColumnUp(x);
                }
            }
        }

        /// <summary>
        /// Finds the first non empty column to the right of the empty target column and 
        /// makes it budge up
        /// </summary>
        /// <param name="emptyTargetColumn"></param>
        private void BudgeNextColumnUp(int emptyTargetColumn)
        {
            for (int x = emptyTargetColumn+1; x < grid.Width; x++)
            {
                if (grid.IsColumnEmpty(x) == false)
                {
                    MoveColumn(x, emptyTargetColumn);

                    break;
                }
            }
        }

        /// <summary>
        /// Moves the column from the given origin grid column position to the target column position
        /// </summary>
        /// <param name="originColumn"></param>
        /// <param name="targetColumn"></param>
        void MoveColumn(int originColumn, int targetColumn)
        {
            var bubbles = GetBubblesInColumn(originColumn);

            foreach (var bubble in bubbles)
            {
                bubble.MoveTo(new Vector3(grid.GetCellPos(targetColumn, 0).x, bubble.transform.position.y, bubble.transform.position.z));
            }
        }

        /// <summary>
        /// Returns all bubbles inside the given column
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        List<Bubble> GetBubblesInColumn(int x)
        {
            List<Bubble> result = new List<Bubble>();

            for (int y = 0; y < grid.Height; y++)
            {
                if (grid.TryGetBubble(x, y, out Bubble bubble))
                {
                    result.Add(bubble);
                }
            }

            return result;
        }
    }
}