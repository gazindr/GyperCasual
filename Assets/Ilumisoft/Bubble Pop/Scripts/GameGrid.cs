namespace Ilumisoft.BubblePop
{
    using UnityEngine;

    public class GameGrid : MonoBehaviour
    {
        [SerializeField]
        int width = 0;

        [SerializeField]
        int height = 0;

        [SerializeField]
        float cellSize = 0.0f;

        /// <summary>
        /// Gets the height (horizontal number of cells) of the grid
        /// </summary>
        public int Width => width;

        /// <summary>
        /// Gets the height (vertical number of cells) of the grid
        /// </summary>
        public int Height => height;

        /// <summary>
        /// Gets the cell size
        /// </summary>
        public float CellSize => cellSize;

        /// <summary>
        /// Gets the center (worl pos) of the bottom left corner of the grid
        /// </summary>
        public Vector3 BottomLeftCorner => new Vector3()
        {
            x = transform.position.x - (width * cellSize) / 2,
            y = transform.position.y - (height * cellSize) / 2,
            z = transform.position.z
        };

        /// <summary>
        /// Returns the position (in world coordinates) of the center of the given cell
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Vector3 GetCellPos(int x, int y)
        {
            return BottomLeftCorner + Vector3.one * CellSize / 2 + new Vector3(x, y, 0) * CellSize;
        }

        /// <summary>
        /// Shows a preview of the grid in the scene view
        /// </summary>
        private void OnDrawGizmos()
        {
            Vector3 cellPos;

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    cellPos = GetCellPos(x, y);

                    Gizmos.DrawWireCube(cellPos, Vector3.one * CellSize);
                }
            }
        }
    }
}