#if UNITY_WEBGL
using InstantGamesBridge;
#endif

using UnityEngine;

namespace KreizTranslation
{
    public class LanguageManager : MonoBehaviour
    {
#if UNITY_WEBGL
        public const string DEFAULT_LANG = "RU";
#else
        public const string DEFAULT_LANG = "ENG";
#endif
        public static LanguageManager Instance;
        public delegate void OnChangeLang();
        public OnChangeLang onChangeLang;

        public TMPro.TMP_FontAsset _Font;
        public Font _Legacy_Font;

        public bool allowRU = true;
        public bool allowEN = true;
        public bool allowCN = false;
        public bool allowTR = true;
        public bool allowJA = false;
        public bool allowDE = true;
        public bool allowES = true;
        public bool allowID = true;
        public bool allowFR = true;
        public bool allowHI = false;
        public bool allowAR = false;
        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                return;

#if UNITY_WEBGL
            if (Bridge.platform.language == "ru" && allowRU)
            {
                ChangeLanguageToRU();
                return;
            }
            else if (Bridge.platform.language == "en" && allowEN)
            {
                ChangeLanguageToENG();
                return;
            }
            else if (Bridge.platform.language == "zh" && allowCN)
            {
                ChangeLanguageToCN();
                return;
            }
            else if (Bridge.platform.language == "tr" && allowTR)
            {
                ChangeLanguageToTR();
                return;
            }
            else if (Bridge.platform.language == "ja" && allowJA)
            {
                ChangeLanguageToJP();
                return;
            }
            else if (Bridge.platform.language == "de" && allowDE)
            {
                ChangeLanguageToDE();
                return;
            }
            else if (Bridge.platform.language == "es" && allowES)
            {
                ChangeLanguageToES();
                return;
            }
            else if (Bridge.platform.language == "id" && allowID)
            {
                ChangeLanguageToID();
                return;
            }
            else if (Bridge.platform.language == "fr" && allowFR)
            {
                ChangeLanguageToFR();
                return;
            }
            else if (Bridge.platform.language == "hi" && allowHI)
            {
                ChangeLanguageToHI();
                return;
            }
            else if (Bridge.platform.language == "ar" && allowAR)
            {
                ChangeLanguageToAR();
                return;
            }
#endif

            if (!PlayerPrefs.HasKey("Language"))
            {
                if (Application.systemLanguage == SystemLanguage.Russian && allowRU)
                {
                    ChangeLanguageToRU();
                }
                else if (Application.systemLanguage == SystemLanguage.English && allowEN)
                {
                    ChangeLanguageToENG();
                }
                else if (Application.systemLanguage == SystemLanguage.Spanish && allowES)
                {
                    ChangeLanguageToES();
                }
                else if (Application.systemLanguage == SystemLanguage.German && allowDE)
                {
                    ChangeLanguageToDE();
                }
                else if (Application.systemLanguage == SystemLanguage.Turkish && allowTR)
                {
                    ChangeLanguageToTR();
                }
                else if (Application.systemLanguage == SystemLanguage.Japanese && allowJA)
                {
                    ChangeLanguageToJP();
                }
                else if ((Application.systemLanguage == SystemLanguage.ChineseSimplified ||
                    Application.systemLanguage == SystemLanguage.ChineseTraditional ||
                    Application.systemLanguage == SystemLanguage.Chinese) && allowCN)
                {
                    ChangeLanguageToCN();
                }
                else if (Application.systemLanguage == SystemLanguage.Indonesian && allowID)
                {
                    ChangeLanguageToID();
                }
                else if (Application.systemLanguage == SystemLanguage.French && allowFR)
                {
                    ChangeLanguageToFR();
                }
                else if (Application.systemLanguage == SystemLanguage.Hindi && allowHI)
                {
                    ChangeLanguageToHI();
                }
                else if (Application.systemLanguage == SystemLanguage.Arabic && allowAR)
                {
                    ChangeLanguageToAR();
                }
                else
                {
                    if (DEFAULT_LANG == "RU")
                    {
                        ChangeLanguageToRU();
                    }
                    else if (DEFAULT_LANG == "CN")
                    {
                        ChangeLanguageToCN();
                    }
                    else
                    {
                        ChangeLanguageToENG();
                    }
                }
            }
        }
        public void ChangeLanguageToRU()
        {
            PlayerPrefs.SetString("Language", "RU");

            RefreshAll();
        }
        public void ChangeLanguageToENG()
        {
            PlayerPrefs.SetString("Language", "ENG");

            RefreshAll();
        }
        public void ChangeLanguageToES()
        {
            PlayerPrefs.SetString("Language", "ES");

            RefreshAll();
        }
        public void ChangeLanguageToDE()
        {
            PlayerPrefs.SetString("Language", "DE");

            RefreshAll();
        }
        public void ChangeLanguageToCN()
        {
            PlayerPrefs.SetString("Language", "CN");

            RefreshAll();
        }
        public void ChangeLanguageToTR()
        {
            PlayerPrefs.SetString("Language", "TR");

            RefreshAll();
        }
        public void ChangeLanguageToJP()
        {
            PlayerPrefs.SetString("Language", "JP");

            RefreshAll();
        }
        public void ChangeLanguageToID()
        {
            PlayerPrefs.SetString("Language", "ID");

            RefreshAll();
        }
        public void ChangeLanguageToFR()
        {
            PlayerPrefs.SetString("Language", "FR");

            RefreshAll();
        }
        public void ChangeLanguageToHI()
        {
            PlayerPrefs.SetString("Language", "HI");

            RefreshAll();
        }
        public void ChangeLanguageToAR()
        {
            PlayerPrefs.SetString("Language", "AR");

            RefreshAll();
        }
        void RefreshAll()
        {
            TextLanguage[] texts = FindObjectsOfType<TextLanguage>();
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].RefreshText();
            }

            ImageLanguage[] imgs = FindObjectsOfType<ImageLanguage>();
            for (int i = 0; i < imgs.Length; i++)
            {
                imgs[i].RefreshImage();
            }

            LanguagePickPanel lpp = FindObjectOfType<LanguagePickPanel>();
            if (lpp != null) lpp.HidePanel();
            onChangeLang?.Invoke();
        }
        public static bool IsRussian()
        {
            return (PlayerPrefs.GetString("Language", DEFAULT_LANG) == "RU");
        }
        public static bool IsEnglish()
        {
            return (PlayerPrefs.GetString("Language", DEFAULT_LANG) == "ENG");
        }
        public static bool IsChinese()
        {
            return (PlayerPrefs.GetString("Language", DEFAULT_LANG) == "CN");
        }
        public static bool IsTurkish()
        {
            return (PlayerPrefs.GetString("Language", DEFAULT_LANG) == "TR");
        }
        public static bool IsJapanese()
        {
            return (PlayerPrefs.GetString("Language", DEFAULT_LANG) == "JP");
        }
        public static bool IsSpanish()
        {
            return (PlayerPrefs.GetString("Language", DEFAULT_LANG) == "ES");
        }
        public static bool IsGerman()
        {
            return (PlayerPrefs.GetString("Language", DEFAULT_LANG) == "DE");
        }
        public static bool IsIndonesian()
        {
            return (PlayerPrefs.GetString("Language", DEFAULT_LANG) == "ID");
        }
        public static string GetLanguage()
        {
            return PlayerPrefs.GetString("Language", DEFAULT_LANG);
        }
    }
}