using UnityEngine;

public partial class AdsManager : MonoBehaviour
{
    public enum RewardType
    {
        Rewarded1
    }

    RewardType activeRewardType = RewardType.Rewarded1;

    public void RewardUser()
    {
        //Debug.LogError("RewardUser:" + activeRewardType.ToString());
        if (activeRewardType == RewardType.Rewarded1)
        {
            MovesManager.Instance.RewardPlayer();
        }
    }
}
