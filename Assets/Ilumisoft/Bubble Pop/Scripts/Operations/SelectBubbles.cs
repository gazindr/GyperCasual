namespace Ilumisoft.BubblePop
{
    using System.Collections;
    using UnityEngine;

    public class SelectBubbles : ICoroutine
    {
        BubbleGrid grid;
        Selection selection;
        Bubble bubble;
        SFXPlayer sfxPlayer;

        public SelectBubbles(BubbleGrid grid, Selection selection, Bubble bubble, SFXPlayer sfxPlayer)
        {
            this.grid = grid;
            this.selection = selection;
            this.bubble = bubble;
            this.sfxPlayer = sfxPlayer;
        }

        public IEnumerator Execute()
        {
            if (grid.IsSelectable(bubble))
            {
                //sfxPlayer.PlaySelectSFX();
                SelectBubble(bubble);
            }

            yield break;
        }

        private void SelectBubble(Bubble bubble)
        {
            // Cancel if bubble is already selected
            if (selection.Selected.Contains(bubble))
            {
                return;
            }

            selection.Selected.Add(bubble);

            SelectNeighbors(bubble);
            
            bubble.PlaySelectAnimation();
        }

        private void SelectNeighbors(Bubble bubble)
        {
            // Disable the collider temporary, otherwise any raycast starting inside will collide with the bubble itself
            bubble.DisableCollider();

            SelectNeighbor(bubble, Vector3.up);
            SelectNeighbor(bubble, Vector3.right);
            SelectNeighbor(bubble, Vector3.down);
            SelectNeighbor(bubble, Vector3.left);

            bubble.EnableCollider();
        }

        private void SelectNeighbor(Bubble bubble, Vector3 direction)
        {
            if (grid.TryGetNeighborWithSameColor(bubble, direction, out var neighbor))
            {
                SelectBubble(neighbor);
            }
        }
    }
}