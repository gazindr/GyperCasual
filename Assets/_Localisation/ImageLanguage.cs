using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace KreizTranslation
{
    public class ImageLanguage : MonoBehaviour
    {
        [SerializeField] Sprite _RU_Sprite;
        [SerializeField] Sprite _ENG_Sprite;

        private void OnEnable()
        {
            RefreshImage();
        }
        void RefreshImage()
        {
            string langStr = PlayerPrefs.GetString("Language", "ENG");

            if (langStr == "RU")
            {
                ChangeTextTo(_RU_Sprite);
            }
            else
            {
                ChangeTextTo(_ENG_Sprite);
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