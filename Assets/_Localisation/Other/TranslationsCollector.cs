using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KreizTranslation
{
    public class TranslationsCollector : MonoBehaviour
    {
        public List<TextLanguage> textLanguages = new List<TextLanguage>();
        [SerializeField, TextArea] public string translationString = "";
        public bool SearchOnlyActiveObjects = true;

        public void GetTextFromComponents()
        {
            translationString = "";

            List<TraslationHelperSimilar> traslationHelperSimilars = new List<TraslationHelperSimilar>();

            for (int i=0; i< textLanguages.Count; i++)
            {
                //   string newText = "" + textLanguages[i]._textRU.Trim('\n') + "|"
                //       + textLanguages[i]._textENG.Trim('\n') + "|" + textLanguages[i]._textCN.Trim('\n');

                Debug.LogError("PleaseFix textLanguages[i].GetAllLanguagesStringToFile()");
                break;
                string newText = "";//textLanguages[i].GetAllLanguagesStringToFile();

                TraslationHelperSimilar similarText = FindSimilarTexts(newText, traslationHelperSimilars);
                if(similarText!=null)
                {
                    similarText.indexes.Add(i);
                    similarText.textLanguagesSimilar.Add(textLanguages[i]);
                    continue;
                }

                similarText = new TraslationHelperSimilar();
                similarText.text = newText;
                similarText.indexes.Add(i);
                similarText.textLanguagesSimilar.Add(textLanguages[i]);

                traslationHelperSimilars.Add(similarText);
            }

            for (int i = 0; i < traslationHelperSimilars.Count; i++)
            {
                translationString += traslationHelperSimilars[i].GetStringToFile() + "\n\n\n";
            }

            
        }

        [System.Serializable]
        public class TraslationHelperSimilar
        {
            public List<TextLanguage> textLanguagesSimilar = new List<TextLanguage>();
            public List<int> indexes = new List<int>();
            public string text = "";

            public string GetStringToFile()
            {
                string indexesString = "";
                for(int i=0; i< indexes.Count;i++)
                {
                    indexesString += indexes[i]+",";
                }
                indexesString = indexesString.Trim(',');

                return indexesString + ")   " + text;
            }
        }

        TraslationHelperSimilar FindSimilarTexts(string searchText,List<TraslationHelperSimilar> _traslationHelperSimilars)
        {
            for(int i=0; i< _traslationHelperSimilars.Count; i++)
            {
                if (_traslationHelperSimilars[i].text == searchText)
                {
                    return _traslationHelperSimilars[i];
                }
            }
            return null;
        }
    }
}