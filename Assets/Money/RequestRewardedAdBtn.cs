using UnityEngine;

public class RequestRewardedAdBtn : MonoBehaviour
{
    [SerializeField] AdsManager.RewardType adType = AdsManager.RewardType.Rewarded1;
    private void Start()
    {
        if(AdsManager.Instance==null)
        {
            Destroy(gameObject);
            return;
        }
    }
    public void OnClick_RequestAd()
    {
        AdsManager.Instance.ShowRewardedAd(adType);
    }
}
