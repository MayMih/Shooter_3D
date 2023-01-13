using UnityEngine;

public class SkinLoader : MonoBehaviour
{
    [Header("Каталог внутри каталога Assets\\Resources")]
    [SerializeField] private string resourceFolderPath = "eggs";

    private Texture2D[] skins;

    public Texture2D GetRandomSkin()
    {
        return skins[Random.Range(0, skins.Length)];
    }

    // Start is called before the first frame update
    private void Awake()
    {
        skins = Resources.LoadAll<Texture2D>(resourceFolderPath);
    }
}
