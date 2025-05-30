using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInteractable : MonoBehaviour
{
    public ObjSO currentObjectSO;
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = new Material(renderer.material);
        }
    }

    public void Interact()
    {
        UIManager.instance.ShowInteractionCanvas(this);
    }

    public void SetColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }

}
