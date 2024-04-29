using UnityEngine;

namespace KreizTranslation
{
    public class NameTag : MonoBehaviour
    {
        public string NameRU = "";
        public string NameENG = "";
        public string NameCN = "";
        public string GetName()
        {
            if (LanguageManager.IsEnglish())
                return NameENG;
            if (LanguageManager.IsChinese())
                return NameCN;
            if (LanguageManager.IsRussian())
                return NameRU;

            return NameRU;
        }
    }
}
