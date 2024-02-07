namespace Ilumisoft.BubblePop
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    public class RevenuePreview : MonoBehaviour
    {
        Text text;

        private void Awake()
        {
            text = GetComponent<Text>();
        }

        private void Start()
        {
            var color = text.color;
            color.a = 0;

            text.CrossFadeColor(color, 0.0f, true, true);
        }

        public void Show(int value)
        {
            var color = text.color;
            color.a = 1;

            text.CrossFadeColor(color, 0.1f, true, true);

            StartCoroutine(ShowCoroutine(value, 0.5f));
        }

        public void Hide()
        {
            var color = text.color;
            color.a = 0;

            text.CrossFadeColor(color, 0.5f, true, true);

        }

        IEnumerator ShowCoroutine(int target, float duration)
        {
            float timeElapsed = 0.0f;

            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;

                int value = (int)Mathf.Lerp(0, target, timeElapsed / duration);

                text.text = string.Format("+{0}", value);

                yield return null;
            }
        }
    }
}