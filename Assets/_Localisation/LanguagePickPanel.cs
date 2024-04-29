using UnityEngine;

public class LanguagePickPanel : MonoBehaviour
{
    [SerializeField] GameObject langButtonsGO;
    public void HidePanel()
    {
        langButtonsGO.SetActive(false);
    }
    public void OnClick_ShowPanel()
    {
        langButtonsGO.SetActive(true);
    }
}
