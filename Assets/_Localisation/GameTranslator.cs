using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace KreizTranslation
{
    public class GameTranslator : MonoBehaviour
    {
        public const char SPLITCHAR = '\'';
        public GameObject UIHolderObject;
        public bool DeleteAllTextLanguages;
        [HideInInspector] public string LabelText = "";
        public TMP_FontAsset FontAsset;

        public void StartTranslationProcess(List<GameObject> sceneObjects)
        {
            StartCoroutine(TranlsationProcedure(sceneObjects));
        }

        //public string defaultLanguage = "ENG";
        [System.Serializable]
        public class TextTranaltion
        {
            public TextLanguage _textLanguage;

            public string basicText;
            public TextTranaltion(TextLanguage textLanguage)
            {
                _textLanguage = textLanguage;
                this.basicText = _textLanguage._text.GetString();
            }
        }
        [SerializeField]
        List<TextTranaltion> textTranaltions = new List<TextTranaltion>();
        [SerializeField] List<TextLanguage> _requireManualTranslation = new List<TextLanguage>();
        public int BatchAmount = 150;
        public int MaxTextLength = 100;
        IEnumerator TranlsationProcedure(List<GameObject> sceneObjects)
        {
            _requireManualTranslation.Clear();
            textTranaltions.Clear();
            textTranaltions = new List<TextTranaltion>();

            int objectsCount = sceneObjects.Count;
            Debug.Log("Found " + objectsCount + " game objects");
            LabelText = "Fetch: Start";
            for (int i = 0; i < objectsCount; i++)
            {
                if (sceneObjects[i].GetComponent<TMP_Text>() || sceneObjects[i].GetComponent<Text>())
                {
                    TextLanguage TL;
                    if (!sceneObjects[i].TryGetComponent<TextLanguage>(out TL))
                    {
                        TL = sceneObjects[i].AddComponent<TextLanguage>();
                        TL.FirstSetup();
                    }
                    if (TL == null)
                        continue;

                    TL.TryTranslate();

                }
                if (i % 1000 == 0)
                {
                    int percent = (int)(((float)i / (float)objectsCount) * 100f);
                    LabelText = "Fetch:" + percent + "%";
                    if (i != 0)
                        Debug.Log("Translation " + i + "/" + objectsCount + " \t \t " + percent);
                    //yield return null;
                }
            }

            LabelText = "Fetch: DONE";
            Debug.Log("Fetched " + objectsCount + " game objects, TextLanugages:==> " + textTranaltions.Count);

            yield return null;



            /*List<TextTranaltion> tempBatchTest = new List<TextTranaltion>(); //TESTING FOR TESTING
            for (int k = 0; k < 7; k++)
            {
                tempBatchTest.Add(textTranaltions[k]);
            }
            yield return StartCoroutine(BatchTranslation(tempBatchTest));*/



            for (int i = 0; i < textTranaltions.Count / BatchAmount; i++)
            {
                List<TextTranaltion> tempBatch = new List<TextTranaltion>();
                for (int k = BatchAmount * i; k < textTranaltions.Count && k < BatchAmount; k++)
                {
                    tempBatch.Add(textTranaltions[k]);
                }
                yield return StartCoroutine(BatchTranslation(tempBatch));
            }

            int doneAmount = (textTranaltions.Count / BatchAmount) * BatchAmount;
            List<TextTranaltion> _tempBatch = new List<TextTranaltion>();
            for (int k = doneAmount; k < textTranaltions.Count; k++)
            {
                _tempBatch.Add(textTranaltions[k]);
            }
            if (_tempBatch.Count > 0)
                yield return StartCoroutine(BatchTranslation(_tempBatch));


            /*for (int i=0; i<textTranaltions.Count; i++)
            {
                startingTextCombined += textTranaltions[i].basicText + "`";
            }
            if(startingTextCombined.Length>0)
                startingTextCombined = startingTextCombined.Substring(0, startingTextCombined.Length - 1);

            LabelText = "String length:"+ startingTextCombined.Length;
            Debug.Log("String length:" + startingTextCombined.Length);*/
            yield return null;



        }
        public IEnumerator BatchTranslation(List<TextTranaltion> textTranaltionsBatch)
        {
            string sourceText = "";
            for (int i = 0; i < textTranaltionsBatch.Count; i++)
            {
                sourceText += textTranaltionsBatch[i].basicText.Trim('\n').Trim(' ') + SPLITCHAR;
            }
            if (sourceText.Length > 0)
                sourceText = sourceText.Substring(0, sourceText.Length - 1);

            string targetLang = "ru";
            //sourceText = sourceText.Replace('!', '|');
            string sourceLang = "auto";
            // Construct the url using our variables and googles api.
            string url = "https://translate.googleapis.com/translate_a/single?client=gtx&sl="
                + sourceLang + "&tl=" + targetLang + "&dt=t&q=" + WWW.EscapeURL(sourceText);

            WWW www = new WWW(url);
            yield return www;
            if (www.isDone)
            {
                Debug.Log("Got ans");
                if (string.IsNullOrEmpty(www.error))
                {
                    string translatedString = www.text;

                    Debug.Log("Text was::::" + sourceText + " \n Transalted::::" + translatedString);

                    translatedString = translatedString.Substring(4);


                    string[] translaters = translatedString.Split(SPLITCHAR);
                    for (int i = 0; i < textTranaltionsBatch.Count; i++)
                    {
                       // textTranaltionsBatch[i]._textLanguage.SetTralsationRU(FixString(translaters[i]));
                    }


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
        /*
         * Thanks for reading and have fun with destructions.'EULA 
     Transalted::::[[["Назад'Обучение'Социальные сети'узнайте, как связаться со мной на веб-сайте'От основ к сов
        етам'КАК ИГРАТЬ:\r\n","Back'Tutorial'Social media'see how to contact me on website'From basics to tips'HOW TO
        PLAY:\r\n",null,null,3,null,null,[[]],[[["c5f104380d2f4c4bf0c587f790a21817","en_ru_2021q4.md"]],[null,
        true]]],["В главном меню нажмите «Играть», затем выберите набор карт для игры и выберите одну из доступ
        ных карт — готово. ","In main menu tap Play then select map bundle to play on and select one of availabl
        e maps - that's it.",null,null,3,null,null,[[]],[[["c5f104380d2f4c4bf0c587f790a21817","en_ru_202
        1q4.md"]]]],["Вы также можете играть на пользовательских картах, созданных другими пользователями, для этого
        при выборе карты нажмите СЛУЧАЙНО - готово!\r\n\r\n","You can also play on custom maps made by other users, 
        to do that when selecting a map click RANDOM - done!\r\n\r\n",null,null,3,null,null,[[]],[[["c5f104380d2f4c4
        bf0c587f790a21817","en_ru_2021q4.md"]],[null,true]]],["Теперь время уничтожить:\r\n","Now time to dest
        roy:\r\n",null,null,3,null,null,[[]],[[["c5f104380d2f4c4bf0c587f790a21817","en_ru_2021q4.md"]],[null,tr
        ue]]],["В правой части экрана есть несколько слотов/панелей быстрого доступа (иконки с инструментами или ч
        то-то еще), которые вы можете выбрать из них. ","On the right side of the screen there are some slots/hotbar (
        icons with tools or whatever) you can chose from them.",null,null,3,null,null,[[]],[[["c5f104380d2f4c4bf0
        c587f790a21817","en_ru_2021q4.md"]]]],["Нажмите кнопку EDIT прямо вверху, чтобы изменить инструменты, 
        которые вы хотите видеть на панели быстрого доступа.\r\n\r\n","Press EDIT button right above to change
        tools you want to be in the hotbar.\r\n\r\n",null,null,3,null,null,[[]],[[["c5f104380d2f4c4bf0c587f790a
        21817","en_ru_2021q4.md"]],[null,true]]],["Хотите что-то сложное?\r\n","Want something challenging?\r\n",null,
        null,3,null,null,[[]],[[["c5f104380d2f4c4bf0c587f790a21817","en_ru_2021q4.md"]],[null,true]]],["При выборе карты
        вы можете включить режим «вызов», и теперь вы не можете спамить и должны планировать каждый выстрел, чтобы 
        максимизировать разрушение.\r\n\r\n","When picking map you can switch\"challenge\" mode ON and now you are
        not able to spam and have to plan each shot to maximise destruction.\r\n\r\n",null,null,3,null,null,[[]],
        [[["c5f104380d2f4c4bf0c587f790a21817","en_ru_2021q4.md"]],[null,true]]],["КАК ИГРАТЬ С ДРУЗЬЯМИ:\r\n","HOW 
        TO PLAY WITH FRIENDS:\r\n",null,null,3,null,null,[[]],[[["c5f104380d2f4c4bf0c587f790a21817","en_ru_2021q4.
        md"]],[null,true]]],["При выборе карты нажмите «МУЛЬТИПЛЕЕР», и вы сможете создавать/присоединяться к комнатам
        в одном и том же облачном регионе. ","When chosing a map press \"MULTIPLAYER\" and there you can create/join 
        rooms in the same cloud region.",null,null,3,null,null,[[]],[[["c5f104380d2f4c4bf0c587f790a21817","en_ru_202
        1q4.md"]]]],["Есть разные регионы? ","Have different regions?",null,null,3,null,null,[[]],[[["c5f104380d2f4c4
        bf0c587f790a21817","en_ru_2021q4.md"]]]],["Попробуйте другое подключение или как-нибудь смените регион ;)\r\
        n\r\n","Try diffrerent connection or somehow change the region ;)\r\n\r\n",null,null,3,null,null,[[]],[[["c5f
        104380d2f4c4bf0c587f790a21817","en_ru_2021q4.md"]],[null,true]]],["КАК СОЗДАТЬ ПОЛЬЗОВАТЕЛЬСКИЕ КАРТЫ:\r\n","HOW T
        O BUILD CUSTOM MAPS:\r\n",null,null,3,null,null,[[]],[[["c5f104380d2f4c4bf0c587f790a21817","en_ru_20
        21q4.md"]],[null,true]]],["Чтобы получить доступ к редактору, просто нажмите «EDITOR» при выборе карт и начнит
        е строить. ","To access editor simply press \"EDITOR\" when picking maps and start building.",null,null,3
        ,null,null,[[]],[[["c5f104380d2f4c4bf0c587f790a21817","en_ru_2021q4.md"]]]],["После того, как вы закончите,
        нажмите кнопку СТАРТ в верхней части экрана и уничтожьте то, что вы только что построили, но, пожалуйста, 
        не забывайте о СОХРАНЕНИИ.\r\n\r\n","After you finished press START button on the top of the screen and 
        destroy what you've just built, but please dont forget about SAVING.\r\n\r\n",null,null,3,null,null,[
        []],[[["c5f104380d2f4c4bf0c587f790a21817","en_ru_2021q4.md"]],[null,true]]],["МАСТЕРСКАЯ:\r\n","WORKSH
        OP:\r\n",null,null,1,null,null,null,[[null,true]]],["Если вы вошли в систему, все сохранения карт также 
        сохраняются на сервере, в меню ДОПОЛНИТЕЛЬНЫЕ ДЕЙСТВИЯ вы можете нажать Загрузить с сервера, и карта с с
       public void TranslateCard(Card card)
        {
            translateCard = card;
            StartCoroutine(Process("en", card.title.languageText[0], 0));
            StartCoroutine(Process("en", card.text.languageText[0], 1));
            for (int i = 0; i < card.conditionalText.Count; i++)
            {
                if (card.conditionalText[i].text.languageText.Length == 0)
                {
                    Debug.Log("No condition text at " + i + " please check that all texts are filled", card);
                    continue;
                }
                string trStr = card.conditionalText[i].text.languageText[0];
                StartCoroutine(Process("en", trStr, 2 + i));
            }


                    string[] translaters = translatedString.Split("\\n\",\"");
                    string perevod = "";
                    for (int i = 0; i < translaters.Length - 1; i += 2)
                    {
                        string[] data = translaters[i].Split("[\"");
                        perevod += data[1];
                    }
        }
        */
    }
}
