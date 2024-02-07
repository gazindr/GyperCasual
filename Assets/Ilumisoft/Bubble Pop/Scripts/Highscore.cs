namespace Ilumisoft.BubblePop
{
    using InstantGamesBridge.Modules.Leaderboard;
    using InstantGamesBridge;
    using UnityEngine;
    
    public static class Highscore
    {
        static readonly string key = "Highscore";

        public static int Value
        {
            get => ReadValue();
            set => SetValue(value);
        }

        static void SetValue(int value)
        {
            PlayerPrefs.SetInt(key, value);

            var leaderboardName = "HighScore";
            var options = new SetScoreYandexOptions(value, leaderboardName);

            // Вариант №1 - просто записать очки игрока
            Bridge.leaderboard.SetScore(options);
            PlayerPrefs.Save();
        }

        static int ReadValue()
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetInt(key);
            }
            else
            {
                return 0;
            }
        }
    }
}