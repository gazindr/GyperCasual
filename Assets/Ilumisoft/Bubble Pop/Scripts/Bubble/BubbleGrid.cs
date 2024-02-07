namespace Ilumisoft.BubblePop
{
    using UnityEngine;

    public class BubbleGrid : GameGrid
    {
        /// <summary>
        /// Reference to the bubble manager
        /// </summary>
        BubbleManager bubbleManager;

        /// <summary>
        /// Reference to the bubble factory
        /// </summary>
        BubbleFactory bubbleFactory;

        private void Awake()
        {
            bubbleManager = FindObjectOfType<BubbleManager>();
            
            bubbleFactory = new BubbleFactory(bubbleManager);
        }

        /// <summary>
        /// Destroys all bubbles
        /// </summary>
        public void Clear()
        {
            foreach(var bubble in bubbleManager.Bubbles)
            {
                if (bubble != null)
                {
                    Destroy(bubble.gameObject);
                }
            }
        }

        /// <summary>
        /// Spawns a random bubble in each cell of the grid
        /// </summary>
        public void SpawnRandomBubbles()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x <Width; x++)
                {
                    SpawnRandomBubble(x, y);
                }
            }
        }

        /// <summary>
        /// Spawns a new bubble in the given cell
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SpawnRandomBubble(int x, int y)
        {
            var bubble = bubbleFactory.Spawn(GetCellPos(x, y));

            bubble.transform.localScale = Vector3.one*CellSize*0.8f;
        }

        /// <summary>
        /// Tries to select the bubble in the given cell. returns true on success, false otherwise
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="bubble"></param>
        /// <returns></returns>
        public bool TryGetBubble(int x, int y, out Bubble bubble)
        {
            var origin = GetCellPos(x, y);

            if (BubbleRaycast(origin, Vector2.zero, Mathf.Infinity, out bubble))
            {
                if (bubble.IsDestroyed == false)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns true if any bubble in the grid is selectable
        /// </summary>
        /// <returns></returns>
        public bool HasSelectable()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if(IsSelectable(x, y))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Returns true if the given cell contains a bubble that can be selected, false otherwise
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsSelectable(int x, int y)
        {
            if (TryGetBubble(x, y, out var bubble))
            {
                return IsSelectable(bubble);
            }

            return false;
        }

        /// <summary>
        /// Returns true if the given bubble can be selected, false otherwise
        /// </summary>
        /// <param name="bubble"></param>
        /// <returns></returns>
        public bool IsSelectable(Bubble bubble)
        {
            if (bubble.IsDestroyed || bubble.IsMoving)
            {
                return false;
            }

            bubble.DisableCollider();

            bool result = HasNeighborWithSameColor(bubble);
            
            bubble.EnableCollider();

            return result;
        }

        /// <summary>
        /// Returns true if the bubble has a neighbor with the same color
        /// </summary>
        /// <param name="bubble"></param>
        /// <returns></returns>
        bool HasNeighborWithSameColor(Bubble bubble)
        {
            var directions = new Vector3[] { Vector3.up, Vector3.right, Vector3.left, Vector3.down };

            foreach (var direction in directions)
            {
                if (HasNeighborWithSameColor(bubble, direction))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns true if the bubble has a neighbor in the given direction with the same color
        /// </summary>
        /// <param name="bubble"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        bool HasNeighborWithSameColor(Bubble bubble, Vector3 direction)
        {
            return TryGetNeighborWithSameColor(bubble, direction, out _);
        }

        public bool TryGetNeighborWithSameColor(Bubble bubble, Vector3 direction, out Bubble neighbor)
        {
            if(BubbleRaycast(bubble.transform.position, direction, CellSize, out neighbor))
            {
                if(neighbor.Color == bubble.Color)
                {
                    return true;
                }
            }

            return false;
        }

        bool BubbleRaycast(Vector3 origin, Vector3 direction, float distance, out Bubble bubble)
        {
            bubble = null;

            var hit = Physics2D.Raycast(origin, direction, distance);

            if (hit.collider != null)
            {
                bubble = hit.collider.GetComponentInParent<Bubble>();

                if (bubble != null)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns true if any bubble in the grid is moving
        /// </summary>
        /// <returns></returns>
        public bool IsAnyBubbleMoving()
        {
            foreach (var bubble in bubbleManager.Bubbles)
            {
                if (bubble.IsMoving)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns true if the given column in the grid does not contain any bubbles, false otherwise
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public bool IsColumnEmpty(int x)
        {
            for(int y = 0; y<Height;y++)
            {
                if(TryGetBubble(x,y, out var bubble))
                {
                    if (bubble != null && bubble.IsDestroyed == false && bubble.IsMoving == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}