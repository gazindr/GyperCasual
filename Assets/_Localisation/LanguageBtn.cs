using UnityEngine;

namespace KreizTranslation
{
    public class LanguageBtn : MonoBehaviour
    {
        [SerializeField] bool SetRU;
        [SerializeField] bool SetENG;
        [SerializeField] bool SetES;
        [SerializeField] bool SetDE;
        [SerializeField] bool SetCN;
        [SerializeField] bool SetTR;
        [SerializeField] bool SetJP;
        [SerializeField] bool SetID;
        [SerializeField] bool SetFR;
        [SerializeField] bool SetHI;
        [SerializeField] bool SetAR;
        private void Start()
        {
            if (SetRU)
            {
                gameObject.SetActive(LanguageManager.Instance.allowRU);
            }
            else if (SetENG)
            {
                gameObject.SetActive(LanguageManager.Instance.allowEN);
            }
            else if (SetES)
            {
                gameObject.SetActive(LanguageManager.Instance.allowES);
            }
            else if (SetDE)
            {
                gameObject.SetActive(LanguageManager.Instance.allowDE);
            }
            else if (SetCN)
            {
                gameObject.SetActive(LanguageManager.Instance.allowCN);
            }
            else if (SetTR)
            {
                gameObject.SetActive(LanguageManager.Instance.allowTR);
            }
            else if (SetJP)
            {
                gameObject.SetActive(LanguageManager.Instance.allowJA);
            }
            else if (SetID)
            {
                gameObject.SetActive(LanguageManager.Instance.allowID);
            }
            else if (SetFR)
            {
                gameObject.SetActive(LanguageManager.Instance.allowFR);
            }
            else if (SetHI)
            {
                gameObject.SetActive(LanguageManager.Instance.allowHI);
            }
            else if (SetAR)
            {
                gameObject.SetActive(LanguageManager.Instance.allowAR);
            }
        }
        public void OnClick_SetLanguage()
        {
            if (LanguageManager.Instance == null) return;

            if (SetRU)
            {
                LanguageManager.Instance.ChangeLanguageToRU();
            }
            else if (SetENG)
            {
                LanguageManager.Instance.ChangeLanguageToENG();
            }
            else if (SetES)
            {
                LanguageManager.Instance.ChangeLanguageToES();
            }
            else if (SetDE)
            {
                LanguageManager.Instance.ChangeLanguageToDE();
            }
            else if (SetCN)
            {
                LanguageManager.Instance.ChangeLanguageToCN();
            }
            else if (SetTR)
            {
                LanguageManager.Instance.ChangeLanguageToTR();
            }
            else if (SetJP)
            {
                LanguageManager.Instance.ChangeLanguageToJP();
            }
            else if (SetID)
            {
                LanguageManager.Instance.ChangeLanguageToID();
            }
            else if (SetFR)
            {
                LanguageManager.Instance.ChangeLanguageToFR();
            }
            else if (SetHI)
            {
                LanguageManager.Instance.ChangeLanguageToHI();
            }
            else if (SetAR)
            {
                LanguageManager.Instance.ChangeLanguageToAR();
            }
        }
    }
}