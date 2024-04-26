using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ilumisoft.BubblePop;
using TMPro;

public class MovesManager : MonoBehaviour
{

    public static MovesManager Instance;

    public int depth = 3;
    public GameObject[] rewardImg;

    public TMP_Text depthText;
    public int availableMoves = 0;

    public GameObject prevButton;
    public List<int[,]> saves = new List<int[,]>();

    private void Awake()
    {
        Instance = this;
    }

    public int currentMove = 0;

    public BubbleGrid grid;

    void Start()
    {
        currentMove = 0;
        availableMoves = 0;
        UpdateButton();
    }
    public void PrevButton()
    {
        if (availableMoves == 0)
        {
            AdsManager.Instance.ShowRewardedAd(AdsManager.RewardType.Rewarded1);
        } else
        {
            PreviousMove();
        }
    }
    public void PreviousMove()
    {
        if (saves.Count == 0)
            return;
        currentMove--;
        //depth--;
        grid.Clear();
        availableMoves--;
        grid.GetGrid(saves[saves.Count -1]);
        saves.RemoveAt(saves.Count - 1);
        UpdateButton();
    }

    public void NextMove()
    {
        saves.Add(grid.SaveGrid());
        if (saves.Count > depth)
            saves.RemoveAt(0);
        currentMove++;
        UpdateButton();
    }
    public void RewardPlayer()
    {
        //depth += 3;
        availableMoves += 3;
        //PreviousMove();
        UpdateButton();
    }
    public void UpdateButton()
    {
        if (currentMove == 0)
        {
            prevButton.SetActive(false);
        } else
        {
            prevButton.SetActive(true);
            if (availableMoves == 0)
            {
                rewardImg[0].SetActive(true);
                rewardImg[1].SetActive(true);
                depthText.text = "";
            }
            else
            {
                rewardImg[0].SetActive(false);
                rewardImg[1].SetActive(false);
                depthText.text = availableMoves.ToString();
            }
        }
        
        
    }
}
