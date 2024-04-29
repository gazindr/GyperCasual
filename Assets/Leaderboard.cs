using System.Collections;
using System.Collections.Generic;
using Ilumisoft.BubblePop;
using InstantGamesBridge;
using InstantGamesBridge.Modules.Leaderboard;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public static Leaderboard Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        SetNewLeaderBoard();
    }
    public void SetNewLeaderBoard()

    {
        var score = Highscore.Value;
        var leaderboardName = "HighScore";
        var options = new SetScoreYandexOptions(score, leaderboardName);



        // Вариант №2 - с коллбекос о завершении
        Bridge.leaderboard.SetScore(OnSetScoreCompleted, options);
    }

    private void OnSetScoreCompleted(bool success)
    {
        if (success)
        {
            Debug.Log("Set new highscore: " + Highscore.Value);
        }
        else
        {
            Debug.Log("Error during saving highscore");
        }
    }


}
