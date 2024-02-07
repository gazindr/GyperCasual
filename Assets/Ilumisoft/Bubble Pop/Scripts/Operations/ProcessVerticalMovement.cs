namespace Ilumisoft.BubblePop
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ProcessVerticalMovement : ICoroutine
    {
        /// <summary>
        /// Reference to the grid
        /// </summary>
        BubbleGrid grid;

        /// <summary>
        /// List of all bubbles which has already been processed by the command
        /// </summary>
        List<GameObject> processedBubbles;

        public ProcessVerticalMovement(BubbleGrid grid)
        {
            this.grid = grid;
            processedBubbles = new List<GameObject>();
        }

        public IEnumerator Execute()
        {
            PushBubblesDown();

            yield return new WaitForBubbleMovement(grid);
        }

        /// <summary>
        /// Makes bubbles fall downwards if there are empty cells under them
        /// </summary>
        void PushBubblesDown()
        {
            processedBubbles.Clear();

            for (int x = 0; x < grid.Width; x++)
            {
                PushBubblesInColumnDown(x);
            }
        }

        /// <summary>
        /// Makes all bubbles in the given column fall downwards if there are empty cells under them
        /// </summary>
        void PushBubblesInColumnDown(int column)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                if (IsCellEmptyOrProcessed(column, y))
                {
                    if (TryGetNextUpperBubble(column, y, out Bubble bubble))
                    {
                        bubble.MoveTo(grid.GetCellPos(column, y));
                        processedBubbles.Add(bubble.gameObject);
                    }
                    //We can stop if no upper bubble exists
                    else
                    {
                        return;
                    }
                }
            }  
        }

        /// <summary>
        /// Returns true if the given cell is empty or already processed. If a cell has been processed
        /// we can consider it as empty, because the bubble inisde the cell will be moving downwards
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        bool IsCellEmptyOrProcessed(int x, int y)
        {
            if(grid.TryGetBubble(x, y, out Bubble bubble))
            {
                // Has the bubble already been processed? => Ignore cell
                if(processedBubbles.Contains(bubble.gameObject))
                {
                    return true;
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Tries to find the next upper active bubble which has not already been processed
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="bubble"></param>
        /// <returns></returns>
        bool TryGetNextUpperBubble(int x, int y, out Bubble bubble)
        {
            bubble = null;

            for (int h = y + 1; h <= grid.Height; h++)
            {
                if (grid.TryGetBubble(x, h, out bubble))
                {
                    // Skip destroyed or already processed bubbles
                    if(bubble.IsDestroyed || processedBubbles.Contains(bubble.gameObject))
                    {
                        continue;
                    }

                    return true;
                }
            }

            return false;
        }
    }
}