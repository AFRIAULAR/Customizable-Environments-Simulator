using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewObjSO", menuName = "Interactable/Object")]
public class ObjSO : ScriptableObject
{
    public string objectName;

    [Header("Material Settings")]
    public List<Material> customMaterialsToEdit; // Optional: specific materials to edit   

    [Header("Texture Settings")]
    public bool isTextured = false;
    public Material texturedMaterial; // Reference to identify target material
    public List<TexturedElement> texturedElements; // replaces available

    [Header("Material Settings")]
    public Vector2 defaultTiling = Vector2.one;
}



[System.Serializable]
public class TexturedElement
{
    public Texture mainTexture;
    public Texture normalMap;
}