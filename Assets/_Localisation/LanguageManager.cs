using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KreizTranslation
{
    public class LanguageManager : MonoBehaviour
    {
        public static LanguageManager Instance;
        public delegate void OnChangeLang();
        public OnChangeLang onChangeLang;
        void Awake()
        {
            if (Instance == null)
                Instance = this;
            if (!PlayerPrefs.HasKey("Language"))
            {
                if (Application.systemLanguage == SystemLanguage.Russian)
                {
                    ChangeLanguageToRU();
                }
                else
                {
                    ChangeLanguageToENG();
                }
            }
        }
        public void ChangeLanguageToRU()
        {
            PlayerPrefs.SetString("Language", "RU");
            TextLanguage[] texts = FindObjectsOfType<TextLanguage>();
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].RefreshText();
            }
            onChangeLang?.Invoke();
        }
        public void ChangeLanguageToENG()
        {
            PlayerPrefs.SetString("Language", "ENG");
            TextLanguage[] texts = FindObjectsOfType<TextLanguage>();
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].RefreshText();
            }
            onChangeLang?.Invoke();
        }
    }
}