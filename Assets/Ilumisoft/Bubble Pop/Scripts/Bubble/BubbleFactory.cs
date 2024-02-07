namespace Ilumisoft.BubblePop
{
    using UnityEngine;

    public class BubbleFactory
    {
        BubbleManager bubbleManager;

        GameObject container;

        public BubbleFactory(BubbleManager bubbleManager)
        {
            this.container = new GameObject("Bubbles");
            this.bubbleManager = bubbleManager;
        }

        public Bubble Spawn(Vector3 position)
        {
            var prefab = bubbleManager.GetRandomPrefab();

            return Spawn(prefab, position);
        }

        Bubble Spawn(Bubble prefab, Vector3 position)
        {
            var bubble = Object.Instantiate(prefab, position, Quaternion.identity);

            bubble.transform.SetParent(container.transform);

            bubbleManager.Register(bubble);

            return bubble;
        }
    }
}