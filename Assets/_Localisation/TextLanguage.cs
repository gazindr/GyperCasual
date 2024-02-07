using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
namespace KreizTranslation
{
    public class TextLanguage : MonoBehaviour
    {
        [SerializeField, TextArea] string _textRU;
        [SerializeField, TextArea] string _textENG;
        string curTextLang = "-";
        // Start is called before the first frame update
        void Start()
        {
            RefreshText();
        }
        private void OnEnable()
        {
            RefreshText();
            //LanguageManager.Instance.onChangeLang += RefreshText;
        }
        private void OnDisable()
        {
            //LanguageManager.Instance.onChangeLang -= RefreshText;
        }
        public void RefreshText()
        {
            string langStr = PlayerPrefs.GetString("Language", "ENG");
            if (langStr == curTextLang) return;

            if (langStr == "RU")
            {
                ChangeTextTo(_textRU);
            }
            else
            {
                ChangeTextTo(_textENG);
            }
        }
        void ChangeTextTo(string _text)
        {
            if (TryGetComponent<TMP_Text>(out TMP_Text tmpCtext))
            {
                tmpCtext.text = _text;
            }
            if (TryGetComponent<Text>(out Text uiCtext))
            {
                uiCtext.text = _text;
            }
        }
        public void FirstSetup()
        {
            string mainText = GetDefaultText();
            _textENG = mainText;
            if (mainText.Length == 0)
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                    DestroyImmediate(this);
#else
            Destroy(this);
#endif
            }
            int digits = 0;
            int letters = 0;
            int specials = 0;
            for (int i = 0; i < mainText.Length; i++)
            {
                //string _checkSymbol = mainText[i].ToString();
                char _checkChar = mainText[i];
                if (Char.IsDigit(_checkChar))
                {
                    digits++;
                }
                else if (Char.IsLetter(_checkChar))
                {
                    letters++;
                }
                else
                {
                    specials++;
                }
            }
            if (letters == 0)
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                    DestroyImmediate(this);
#else
            Destroy(this);
#endif
            }
        }
        public string GetDefaultText()
        {
            if (TryGetComponent<TMP_Text>(out TMP_Text tmpCtext))
            {
                if (tmpCtext.text.Length == 0)
                {
                    return _textENG;
                }
                return tmpCtext.text.Trim('\n');
            }
            if (TryGetComponent<Text>(out Text uiCtext))
            {
                if (uiCtext.text.Length == 0)
                {
                    return _textENG;
                }
                return uiCtext.text.Trim('\n');
            }

            return "";
        }
        public void SetTralsationENG(string newText)
        {
            if (newText.Length == 0) return;
            translated = true;
            _textENG = newText;
        }
        public void SetTralsationRU(string newText)
        {
            if (newText.Length == 0) return;
            translated = true;
            _textRU = newText;
        }
        [SerializeField] bool translated = false;
        public bool IsTranslated()
        {
            if (_textRU.Length == 0) return false;
            if (_textENG.Length == 0) return false;
            int diff = Mathf.Abs(_textRU.Length - _textENG.Length);
            if ((float)diff / (float)Mathf.Max(_textRU.Length, _textENG.Length) < 0.5f)
            {
                //Debug.Log("diff:" + diff + " max:" + Mathf.Max(_textRU.Length, _textENG.Length), gameObject);
                return true;
            }
            return translated;
        }
        public void UpdateFont(TMP_FontAsset font, Font fontBasic)
        {
            if (TryGetComponent<TMP_Text>(out TMP_Text tmpCtext))
            {
                if(font!=null)
                    tmpCtext.font = font;
            }
            else if (TryGetComponent<Text>(out Text uiCtext))
            {
                if(fontBasic!=null)
                    uiCtext.font = fontBasic;
            }
        }
        public void DisplayRU()
        {
            if (TryGetComponent<TMP_Text>(out TMP_Text tmpCtext))
            {
                tmpCtext.text = _textRU;
            }
            else if (TryGetComponent<Text>(out Text uiCtext))
            {
                uiCtext.text = _textRU;
            }
        }
        public void DisplayENG()
        {
            if (TryGetComponent<TMP_Text>(out TMP_Text tmpCtext))
            {
                tmpCtext.text = _textENG;
            }
            else if (TryGetComponent<Text>(out Text uiCtext))
            {
                uiCtext.text = _textRU;
            }
        }
        public void StartTranslationProcessToENG()
        {
            string textToTranslate = _textRU;
            if (textToTranslate.Length == 0)
                textToTranslate = GetDefaultText();
            StartCoroutine(Translation(GetDefaultText(), "en"));
        }
        public void StartTranslationProcessToRU()
        {
            string textToTranslate = _textENG;
            if (textToTranslate.Length == 0)
                textToTranslate = GetDefaultText();
            StartCoroutine(Translation(textToTranslate, "ru"));
            
        }
        public IEnumerator Translation(string sourceText, string targetLang)
        {
            //sourceText = sourceText.Replace('!', '|');
            string sourceLang = "auto";
            // Construct the url using our variables and googles api.
            string url = "https://translate.googleapis.com/translate_a/single?client=gtx&sl="
                + sourceLang + "&tl=" + targetLang + "&dt=t&q=" + WWW.EscapeURL(sourceText);

            WWW www = new WWW(url);
            yield return www;
            if (www.isDone)
            {
                //Debug.Log("Got ans");
                if (string.IsNullOrEmpty(www.error))
                {
                    string translatedString = www.text;

                    Debug.Log("Text was::::" + sourceText + " \n Transalted::::" + translatedString);

                    translatedString = translatedString.Substring(4);

                    if(targetLang=="ru")
                        SetTralsationRU(FixString(translatedString));
                    else if (targetLang == "en")
                        SetTralsationENG(FixString(translatedString));
                }
                else
                {
                    Debug.Log("ERROR:" + www.error);
                    Debug.LogError("ERROR:" + www.error);
                }
            }
        }
        string FixString(string strToFix)
        {
            if (!strToFix.Contains("\\n\",\""))
            {
                return FixStringFinal(strToFix);
            }
            else
            {
                string newFixedStr = "";
                string[] translaters = strToFix.Split("\\n\",\"");
                for (int i = 0; i < translaters.Length; i++)
                {
                    string[] data = translaters[i].Split(",[\"");

                    for (int k = 0; k < data.Length; k += 2)
                    {
                        newFixedStr = data[k];
                    }
                }
                strToFix = FixStringFinal(newFixedStr);
            }
            return strToFix;
        }
        string FixStringFinal(string strToFix)
        {
            if (!strToFix.Contains("\",\""))
            {
                return strToFix;
            }
            else
            {
                string[] translaters = strToFix.Split("\",\"");
                strToFix = translaters[0];
            }

            return strToFix;
        }
    }
}