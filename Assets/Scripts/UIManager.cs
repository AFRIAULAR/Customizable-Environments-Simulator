using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public bool isUIOpen = false;
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}