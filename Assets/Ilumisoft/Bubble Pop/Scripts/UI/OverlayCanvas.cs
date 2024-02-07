namespace Ilumisoft.BubblePop
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Playables;

    public class OverlayCanvas : MonoBehaviour
    {
        [SerializeField]
        PlayableDirector playableDirector = null;

        [SerializeField]
        PlayableAsset fadeInTimeline = null;

        [SerializeField]
        PlayableAsset fadeOutTimeline = null;

        /// <summary>
        /// This can be used in a coroutine to smoothly fade the overlay in
        /// </summary>
        /// <returns></returns>
        public IEnumerator FadeIn()
        {
            playableDirector.playableAsset = fadeInTimeline;
            playableDirector.Play();

            yield return new WaitForSecondsRealtime((float)fadeOutTimeline.duration);
        }

        /// <summary>
        /// This can be used in a coroutine to smoothly fade the overlay out
        /// </summary>
        /// <returns></returns>
        public IEnumerator FadeOut()
        {
            playableDirector.playableAsset = fadeOutTimeline;
            playableDirector.Play();

            yield return new WaitForSecondsRealtime((float)fadeOutTimeline.duration);
        }
    }
}