namespace Ilumisoft.BubblePop
{
    using System.Collections;
    using UnityEngine;

    public abstract class GameMode : MonoBehaviour
    {
        public abstract IEnumerator StartGame();

        public abstract IEnumerator RunGame();

        public abstract IEnumerator EndGame();
    }
}