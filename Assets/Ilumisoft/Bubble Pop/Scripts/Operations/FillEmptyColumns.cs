namespace Ilumisoft.BubblePop
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class FillEmptyColumns : ICoroutine
    {
        BubbleGrid grid;

        public FillEmptyColumns(BubbleGrid grid) 
        {
            this.grid = grid;
        }

        public IEnumerator Execute()
        {
            List<int> emptyColumns = FindEmptyColumns();
            
            if (emptyColumns.Count>0)
            {
                SpawnColumns(emptyColumns);

                yield return new WaitForSeconds(0.5f);
            }
        }

        /// <summary>
        /// Gets a list of all empty columns in the grid
        /// </summary>
        /// <returns></returns>
        List<int> FindEmptyColumns()
        {
            List<int> emptyColumns = new List<int>();

            for (int x = 0; x < grid.Width; x++)
            {
                if (grid.IsColumnEmpty(x))
                {
                    emptyColumns.Add(x);
                }
            }

            return emptyColumns;
        }

        /// <summary>
        /// Spawns new bubbles in each columns in the given list
        /// </summary>
        /// <param name="columns"></param>
        void SpawnColumns(List<int> columns)
        {
            foreach(int column in columns)
            {
                SpawnColumn(column);
            }
        }

        /// <summary>
        /// Spawns new bubbles in the given grid column
        /// </summary>
        /// <param name="x"></param>
        void SpawnColumn(int x)
        {
            for(int y = 0; y<grid.Height; y++)
            {
                grid.SpawnRandomBubble(x, y);
            }
        }
    }
}