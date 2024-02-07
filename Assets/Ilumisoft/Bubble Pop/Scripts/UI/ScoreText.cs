namespace Ilumisoft.BubblePop
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Text))]
    public class ScoreText : MonoBehaviour
    {
        Text text;

        int lastScore = 0;

        private void Awake()
        {
            text = GetComponent<Text>();
        }

        private void OnEnable()
        {
            Score.OnScoreChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            Score.OnScoreChanged -= OnScoreChanged;
        }

        private void Start()
        {
            text.text = "0";
        }

        private void OnScoreChanged(int score)
        {
            StartCoroutine(IncreaseScore(lastScore, score, 1.0f));

            lastScore = score;
        }

        IEnumerator IncreaseScore(int from, int to, float duration)
        {
            float timeElapsed = 0.0f;

            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;

                int value = (int)Mathf.Lerp(from, to, timeElapsed / duration);

                text.text = value.ToString();

                yield return null;
            }
        }
    }
}