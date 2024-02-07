namespace Ilumisoft.BubblePop
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class GameOverUI : MonoBehaviour
    {
        [SerializeField]
        Text scoreText = null;

        [SerializeField]
        //Text highscoreText = null;
        public TMP_Text highscoreText;
        [SerializeField]
        GameObject newHighscoreMessage = null;

        void Start()
        {
            if (HasNewHighscore())
            {
                UpdateHighscore();
                DisplayHighscore(false);
                DisplayNewHighscoreMessage(true);
            }
            else
            {
                DisplayHighscore(true);
                DisplayNewHighscoreMessage(false);
            }

            DisplayScore();
        }

        bool HasNewHighscore()
        {
            return Score.Value > Highscore.Value;
        }

        void UpdateHighscore()
        {
            Highscore.Value = Score.Value;
        }

        void DisplayNewHighscoreMessage(bool show)
        {
            newHighscoreMessage.SetActive(show);
        }

        void DisplayHighscore(bool show)
        {
            if (show)
            {
                if (PlayerPrefs.GetString("Language") == "RU")
                {
                    highscoreText.text = $"ЛУЧШИЙ\n{Highscore.Value}";
                }
                else
                {
                    highscoreText.text = $"BEST\n{Highscore.Value}";
                }
                
            }
            else
            {
                highscoreText.text = string.Empty;
            }
        }

        void DisplayScore()
        {
            scoreText.text = Score.Value.ToString();
        }
    }
}
