using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInteractable : MonoBehaviour
{
    public ObjSO currentObjectSO;
    public void Interact()
    {
        Debug.Log($"Interacci�n con: {currentObjectSO.objectName}");
       
        //Buscar como hacerlo con tags
    }
}
