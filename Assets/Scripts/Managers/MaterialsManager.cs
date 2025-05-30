using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MaterialsManager : MonoBehaviour
{
    public static MaterialsManager instance;

    public List<Material> availableMaterials; //from inspector
    public List<Texture2D> availableTextures;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public Material GetMaterialByIndex(int index)
    {
        if (index >= 0 && index < availableMaterials.Count)
            return availableMaterials[index];
        return null;
    }

    public Texture2D GetTextureByIndex(int index)
    {
        if (index >= 0 && index < availableTextures.Count)
            return availableTextures[index];
        return null;
    }
}
