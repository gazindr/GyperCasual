namespace Ilumisoft.BubblePop
{
    using UnityEngine;

    public class GameUI : MonoBehaviour
    {
        [SerializeField]
        RevenuePreview preview = null;

        public void ShowScorePreview(int value)
        {
            preview.Show(value);
        }

        public void HideScorePreview()
        {
            preview.Hide();
        }
    }
}