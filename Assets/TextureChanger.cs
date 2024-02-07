using UnityEngine;

public class TextureChanger : MonoBehaviour
{
    public static TextureChanger Instance;

    private void Awake()
    {
        Instance = this;
    }
    public Material material;

    public  void ChangeTexture(Texture _texture)
    {
        material.mainTexture = _texture;
    }
}
