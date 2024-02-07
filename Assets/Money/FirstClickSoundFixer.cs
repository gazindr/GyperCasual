using UnityEngine;

public class FirstClickSoundFixer : MonoBehaviour
{
#if UNITY_WEBGL
    void Start()
    {
        if (Time.unscaledTime > 3)
        {
            Destroy(this);
            return;
        }
        AudioListener.pause = true;
        Debug.Log("Muted");
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Clicked();
        }
    }
    void Clicked()
    {
        Debug.Log("UnMuted");
        AudioListener.pause = false;
        Destroy(this);
    }
#endif
}
