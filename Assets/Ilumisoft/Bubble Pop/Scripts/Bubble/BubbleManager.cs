namespace Ilumisoft.BubblePop
{
    using System.Collections.Generic;
    using UnityEngine;

    public class BubbleManager : MonoBehaviour
    {
        [SerializeField]
        List<Bubble> prefabs = new List<Bubble>();

        public List<Bubble> Bubbles { get; } = new List<Bubble>();

        public Bubble GetRandomPrefab()
        {
            return prefabs.GetRandom();
        }

        public void Register(Bubble bubble)
        {
            bubble.OnDestroy += OnBubbleDestroy;
            Bubbles.Add(bubble);
        }

        public void Deregister(Bubble bubble)
        {
            bubble.OnDestroy -= OnBubbleDestroy;
            Bubbles.Remove(bubble);
        }

        private void OnBubbleDestroy(Bubble bubble)
        {
            Deregister(bubble);
        }
    }
}