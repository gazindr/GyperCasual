namespace Ilumisoft.BubblePop
{
    using TMPro;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UI;
    using static UnityEngine.GraphicsBuffer;

    public class GameOverUI : MonoBehaviour
    {
        [SerializeField]
        Text scoreText = null;

        [SerializeField]
        //Text highscoreText = null;
        public TMP_Text highscoreText;
        [SerializeField]
        GameObject newHighscoreMessage = null;
        public TranslatableString highscoreString;

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
            Leaderboard.Instance.SetNewLeaderBoard();
        }

        void DisplayNewHighscoreMessage(bool show)
        {
            newHighscoreMessage.SetActive(show);
        }

        void DisplayHighscore(bool show)
        {
            if (show)
            {
                
                highscoreText.text = highscoreString.GetString() + " " + Highscore.Value;
                
                
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

#if UNITY_EDITOR
    [CustomEditor(typeof(GameOverUI))]
    public class TutaClassEditor : Editor
    {
        GameOverUI myTarget;
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            myTarget = (GameOverUI)target;


            if (GUILayout.Button("Translate"))
            {
                myTarget.highscoreString.TranslateAll();
            }
            

        }
    }
#endif 
}
