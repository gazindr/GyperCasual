using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace KreizTranslation
{
    public class TextLanguage : MonoBehaviour
    {
        public TranslatableString _text;
        string curTextLang = "-";
        // Start is called before the first frame update
        void Start()
        {
            RefreshText();
        }
        private void OnEnable()
        {
            if (LanguageManager.Instance)
                RefreshText();
            //LanguageManager.Instance.onChangeLang += RefreshText;
        }
        private void OnDisable()
        {
            //LanguageManager.Instance.onChangeLang -= RefreshText;
        }
        public void RefreshText()
        {
            string langStr = LanguageManager.GetLanguage();
            if (langStr == curTextLang) return;

            if (TryGetComponent<TMP_Text>(out TMP_Text tmpCtext) && LanguageManager.Instance)
            {
                tmpCtext.font = LanguageManager.Instance._Font;
            }
            else if (TryGetComponent<Text>(out Text legacyCtext) && LanguageManager.Instance)
            {
                legacyCtext.font = LanguageManager.Instance._Legacy_Font;
            }

            ChangeTextTo(_text.GetString());
        }

        void ChangeTextTo(string _text)
        {
            if (TryGetComponent<TMP_Text>(out TMP_Text tmpCtext))
            {
                tmpCtext.text = _text;
            }
            else if (TryGetComponent<Text>(out Text uiCtext))
            {
                uiCtext.text = _text;
            }
        }
        public void FirstSetup()
        {
            string mainText = _text.GetString();
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
        public void UpdateFont(TMP_FontAsset font)
        {
            if (TryGetComponent<TMP_Text>(out TMP_Text tmpCtext))
            {
                tmpCtext.font = font;
            }
        }
        public void DisplayRU()
        {
            if (TryGetComponent<TMP_Text>(out TMP_Text tmpCtext))
            {
                //tmpCtext.text = _textRU;
            }
            else if (TryGetComponent<Text>(out Text uiCtext))
            {
                //uiCtext.text = _textRU;
            }
        }
        public void DisplayENG()
        {
            if (TryGetComponent<TMP_Text>(out TMP_Text tmpCtext))
            {
                //tmpCtext.text = _textENG;
            }
            else if (TryGetComponent<Text>(out Text uiCtext))
            {
                //uiCtext.text = _textRU;
            }
        }
        public void DisplayCN()
        {
            if (TryGetComponent<TMP_Text>(out TMP_Text tmpCtext))
            {
                //tmpCtext.text = _textCN;
            }
            else if (TryGetComponent<Text>(out Text uiCtext))
            {
                //uiCtext.text = _textCN;
            }
        }
        public void TryTranslate()
        {

        }
        public void TranslateMissing()
        {
            _text.TranslateMissing(MainLanguageTranslated);
        }
        public void TranslateFromRU()
        {
            _text.TranslateFromRU(MainLanguageTranslated);
        }
        public void TranslateFromENG()
        {
            _text.TranslateFromENG(MainLanguageTranslated);
        }
        public void TranslateAll()
        {
            _text.TranslateAll(MainLanguageTranslated);
        }
        public void TranslateFromCOMPONENT()
        {
            string maintext = "";
            if (TryGetComponent<TMP_Text>(out TMP_Text tmpCtext))
            {
                maintext = tmpCtext.text;
            }
            else if (TryGetComponent<Text>(out Text uiCtext))
            {
                maintext = uiCtext.text;
            }
            _text.TranslateFromString(maintext);
        }
        public bool HasDifferentComponentText()
        {
            if (TryGetComponent<TMP_Text>(out TMP_Text tmpCtext))
            {
                if (_text._textENG != tmpCtext.text && _text._textRU != tmpCtext.text)
                    return true;
            }
            else if (TryGetComponent<Text>(out Text uiCtext))
            {
                if (_text._textENG != uiCtext.text && _text._textRU != uiCtext.text)
                    return true;
            }
            return false;
        }
        public void MainLanguageTranslated()
        {
#if UNITY_EDITOR
            if(!Application.isPlaying)
            {
                if (TryGetComponent<TMP_Text>(out TMP_Text tmpCtext))
                {
                    tmpCtext.text = _text._textENG;
                    EditorUtility.SetDirty(tmpCtext);
                }
                else if (TryGetComponent<Text>(out Text uiCtext))
                {
                    uiCtext.text = _text._textENG;
                    EditorUtility.SetDirty(uiCtext);
                }
            }
#endif
        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(TextLanguage))]
    public class TextLanguageEditor : Editor
    {
        TextLanguage myTarget;
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            myTarget = (TextLanguage)target;

            if (myTarget.HasDifferentComponentText())
            {
                if (GUILayout.Button("Translate from COMPONENT"))
                {
                    myTarget.TranslateFromCOMPONENT();
                }
            }

            if (myTarget._text.HasMissing())
            {
                if (GUILayout.Button("Translate Missing"))
                {
                    myTarget.TranslateMissing();
                }
            }

            if (myTarget._text._textRU != null)
            {
                if(myTarget._text._textRU.Length>0)
                {
                    if (GUILayout.Button("Translate from RU"))
                    {
                        myTarget.TranslateFromRU();
                    }
                }
            }
            if (myTarget._text._textENG != null)
            {
                if (myTarget._text._textENG.Length > 0)
                {
                    if (GUILayout.Button("Translate from ENG"))
                    {
                        myTarget.TranslateFromENG();
                    }
                }
            }

            /*if (GUILayout.Button("Translate ALL"))
            {
                myTarget.TranslateAll();
            }*/
        }
    }
#endif
}