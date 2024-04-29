using UnityEngine;
using UnityEngine.UI;
namespace KreizTranslation
{
    public class ImageLanguage : MonoBehaviour
    {
        [SerializeField] Sprite _RU_Sprite;
        [SerializeField] Sprite _ENG_Sprite;
        [SerializeField] Sprite _ES_Sprite;
        [SerializeField] Sprite _DE_Sprite;
        [SerializeField] Sprite _TR_Sprite;
        [SerializeField] Sprite _CN_Sprite;
        [SerializeField] Sprite _JP_Sprite;
        [SerializeField] Sprite _ID_Sprite;

        private void OnEnable()
        {
            RefreshImage();
        }
        public void RefreshImage()
        {
            string langStr = LanguageManager.GetLanguage();

            if (langStr == "RU")
            {
                ChangeTextTo(_RU_Sprite);
            }
            else if (langStr == "ENG")
            {
                ChangeTextTo(_ENG_Sprite);
            }
            else if (langStr == "ES")
            {
                ChangeTextTo(_ES_Sprite);
            }
            else if (langStr == "DE")
            {
                ChangeTextTo(_DE_Sprite);
            }
            else if (langStr == "TR")
            {
                ChangeTextTo(_TR_Sprite);
            }
            else if (langStr == "CN")
            {
                ChangeTextTo(_CN_Sprite);
            }
            else if (langStr == "JP")
            {
                ChangeTextTo(_JP_Sprite);
            }
            else if (langStr == "ID")
            {
                ChangeTextTo(_ID_Sprite);
            }
        }
        void ChangeTextTo(Sprite _sprite)
        {
            if (TryGetComponent<Image>(out Image _imgComponent))
            {
                _imgComponent.sprite = _sprite;
            }
        }
    }
}