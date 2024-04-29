using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class EditorTralsateHelper : MonoBehaviour
{
    public static GameObject Translate(string sourceText, string targetLang, UnityAction<string> callBack, UnityAction extracallBack = null)
    {
        GameObject newGO = new GameObject();
        //Debug.Log("ST:" + sourceText);
        newGO.transform.name = "T--"+sourceText.Substring(0,Mathf.Min(5, sourceText.Length-1));
        EditorTralsateHelper dc = newGO.AddComponent<EditorTralsateHelper>();
        dc.StartCoroutine(dc.Translation(sourceText, targetLang, callBack, extracallBack));
        return newGO;
    }
    public IEnumerator Translation(string sourceText, string targetLang, UnityAction<string> callBack, UnityAction extracallBack = null)
    {
        //sourceText = sourceText.Replace('!', '|');
        sourceText = sourceText.Replace("\n", "*%*");
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
                translatedString = translatedString.Replace("*%*", "\n");
                //Debug.Log("Text was::::" + sourceText + " \n Transalted::::" + translatedString);

                translatedString = translatedString.Substring(4);

                callBack.Invoke(FixString(translatedString));
                extracallBack?.Invoke();
#if UNITY_EDITOR
                EditorUtility.SetDirty(this);
#endif
            }
            else
            {
                Debug.Log("ERROR:" + www.error);
                Debug.LogError("ERROR:" + www.error);
            }
        }
#if UNITY_EDITOR
        if (!Application.isPlaying)
            DestroyImmediate(gameObject);
        else
            Destroy(gameObject);
#else
            Destroy(gameObject);
#endif
    }
    string FixString(string strToFix)
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
