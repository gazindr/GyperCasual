using KreizTranslation;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TranslatableString
{
    [SerializeField, TextArea] public string _textRU = "";
    [SerializeField, TextArea] public string _textENG = "";
    //[HideInInspector]
    [SerializeField, TextArea] public string _textES = "";
    [SerializeField, TextArea] public string _textDE = "";
    //[HideInInspector]
    [SerializeField, TextArea] public string _textCN = "";
    //[HideInInspector]
    [SerializeField, TextArea] public string _textTR = "";
    //[HideInInspector]
    [SerializeField, TextArea] public string _textJP = "";
    //[HideInInspector]
    [SerializeField, TextArea] public string _textID = "";
    //[HideInInspector]
    [SerializeField, TextArea] public string _textFR = "";
    //[HideInInspector]
    [SerializeField, TextArea] public string _textHI = "";
    //[HideInInspector]
    [SerializeField, TextArea] public string _textAR = "";
    string translatedFrom = "";

    public string GetString()
    {
        string langStr = LanguageManager.GetLanguage();

        if (langStr == "RU") return _textRU;

        if (langStr == "ENG") return _textENG;

        if (langStr == "ES") return (_textES);

        if (langStr == "DE") return (_textDE);

        if (langStr == "CN") return (_textCN);

        if (langStr == "TR") return (_textTR);
            
        if (langStr == "JP") return (_textJP);

        if (langStr == "ID") return (_textID);

        if (langStr == "FR") return (_textFR);

        if (langStr == "HI") return (_textHI);

        if (langStr == "AR") return (_textAR);

        Debug.LogError("No Language String for l=" + langStr);
        return _textRU;
    }
    
    public void TranslateAll(UnityAction callBack = null)
    {
        string mainText = _textENG;
        if (AlredyTranslated(mainText))
        {
            Debug.Log("Already translated" + mainText+" || "+translatedFrom);
            return;
        }
        if (mainText.Length == 0)
        {
            mainText = _textRU;
            if (mainText.Length == 0)
            {
                Debug.Log("Nothing to translate");
                return;
            }
            if (AlredyTranslated(mainText)) return;
            EditorTralsateHelper.Translate(mainText, "en", SetTranslationENG, callBack);
        }
        else
        {
            EditorTralsateHelper.Translate(mainText, "ru", SetTranslationRU);
        }
        TranslateRest(mainText);
        if (mainText.Length > 0) translatedFrom = Hash128.Compute(mainText).ToString();
    }
    public void TranslateFromString(string mainText, UnityAction callBack = null)
    {
        EditorTralsateHelper.Translate(mainText, "ru", SetTranslationRU, callBack);
        EditorTralsateHelper.Translate(mainText, "en", SetTranslationENG, callBack);
        TranslateRest(mainText);
        if (mainText.Length > 0) translatedFrom = Hash128.Compute(mainText).ToString();
    }
    public void TranslateFromRU(UnityAction callBack = null)
    {
        string mainText = _textRU;

        EditorTralsateHelper.Translate(mainText, "en", SetTranslationENG, callBack);
        TranslateRest(mainText);
        if (mainText.Length > 0) translatedFrom = Hash128.Compute(mainText).ToString();
    }
    public void TranslateFromENG(UnityAction callBack = null)
    {
        string mainText = _textENG;

        EditorTralsateHelper.Translate(mainText, "ru", SetTranslationRU, callBack);
        TranslateRest(mainText);
        if (mainText.Length > 0) translatedFrom = Hash128.Compute(mainText).ToString();
    }
    void TranslateRest(string mainText)
    {
        EditorTralsateHelper.Translate(mainText, "zh-CN", SetTranslationCN);
        EditorTralsateHelper.Translate(mainText, "tr", SetTranslationTR);
        EditorTralsateHelper.Translate(mainText, "ja", SetTranslationJA);
        EditorTralsateHelper.Translate(mainText, "es", SetTranslationES);
        EditorTralsateHelper.Translate(mainText, "de", SetTranslationDE);
        EditorTralsateHelper.Translate(mainText, "id", SetTranslationID);
        EditorTralsateHelper.Translate(mainText, "fr", SetTranslationFR);
        EditorTralsateHelper.Translate(mainText, "hi", SetTranslationHI);
        EditorTralsateHelper.Translate(mainText, "ar", SetTranslationAR);
    }
    bool AlredyTranslated(string checkStr)
    {
        return translatedFrom == Hash128.Compute(checkStr).ToString();
        return translatedFrom == checkStr.Substring(0, Mathf.Min(10,checkStr.Length-1));
    }
    public void TranslateMissing(UnityAction callBack = null)
    {
        string mainText = _textENG;
        if (mainText.Length == 0)
        {
            mainText = _textRU;
            if(mainText.Length==0)
            {
                Debug.Log("Nothing to translate eng:"+_textENG+" ru:"+_textRU);
                return;
            }
            EditorTralsateHelper.Translate(mainText, "en", SetTranslationENG, callBack);
        }
        else
        {
            if (_textRU.Length == 0 || !AlredyTranslated(mainText))
            {
                Debug.Log("Translate from TU ::: _textRU.Length" + _textRU.Length + "  mainText:" + mainText + " AlredyTranslated(mainText)" + AlredyTranslated(mainText));
                EditorTralsateHelper.Translate(mainText, "ru", SetTranslationRU, callBack);
            }
        }
        //Debug.Log("_textCN.Length == " + _textCN.Length + "  AT:" + AlredyTranslated(mainText));
        if (_textCN.Length == 0 || !AlredyTranslated(mainText)) EditorTralsateHelper.Translate(mainText, "zh-CN", SetTranslationCN);
        if (_textTR.Length == 0 || !AlredyTranslated(mainText)) EditorTralsateHelper.Translate(mainText, "tr", SetTranslationTR);
        if (_textJP.Length == 0 || !AlredyTranslated(mainText)) EditorTralsateHelper.Translate(mainText, "ja", SetTranslationJA);
        if (_textES.Length == 0 || !AlredyTranslated(mainText)) EditorTralsateHelper.Translate(mainText, "es", SetTranslationES);
        if (_textDE.Length == 0 || !AlredyTranslated(mainText)) EditorTralsateHelper.Translate(mainText, "de", SetTranslationDE);
        if (_textID.Length == 0 || !AlredyTranslated(mainText)) EditorTralsateHelper.Translate(mainText, "id", SetTranslationID);
        if (_textFR.Length == 0 || !AlredyTranslated(mainText)) EditorTralsateHelper.Translate(mainText, "fr", SetTranslationFR);
        if (_textHI.Length == 0 || !AlredyTranslated(mainText)) EditorTralsateHelper.Translate(mainText, "hi", SetTranslationHI);
        if (_textAR.Length == 0 || !AlredyTranslated(mainText)) EditorTralsateHelper.Translate(mainText, "ar", SetTranslationAR);

        if (mainText.Length > 0) translatedFrom = Hash128.Compute(mainText).ToString();
       // if (mainText.Length>0) translatedFrom = mainText.Substring(0, Mathf.Min(10, mainText.Length - 1));
    }
    public void SetTranslationRU(string _text)
    {
        _textRU = _text;
    }
    public void SetTranslationENG(string _text)
    {
        _textENG = _text;
    }
    public void SetTranslationCN(string _text)
    {
        _textCN = _text;
    }
    public void SetTranslationTR(string _text)
    {
        _textTR = _text;
    }
    public void SetTranslationJA(string _text)
    {
        _textJP = _text;
    }
    public void SetTranslationDE(string _text)
    {
        _textDE = _text;
    }
    public void SetTranslationES(string _text)
    {
        _textES = _text;
    }
    public void SetTranslationID(string _text)
    {
        _textID = _text;
    }
    public void SetTranslationFR(string _text)
    {
        _textFR = _text;
    }
    public void SetTranslationHI(string _text)
    {
        _textHI = _text;
    }
    public void SetTranslationAR(string _text)
    {
        _textAR = _text;
    }
    public bool HasMissing()
    {
        if(!AlredyTranslated(_textRU) && !AlredyTranslated(_textENG)) return true;
        if (_textRU.Length == 0) return true;
        if (_textENG.Length == 0) return true;
        if (_textES.Length == 0) return true;
        if (_textDE.Length == 0) return true;
        if (_textCN.Length == 0) return true;
        if (_textTR.Length == 0) return true;
        if (_textJP.Length == 0) return true;
        if (_textID.Length == 0) return true;
        if (_textFR.Length == 0) return true;
        if (_textHI.Length == 0) return true;
        if (_textAR.Length == 0) return true;
        return false;
    }
}
