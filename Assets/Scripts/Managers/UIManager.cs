using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public bool isUIOpen = false;

    [Header("UI Elements")]
    public GameObject interactionPanel;
    //public Text objectNameText;
    public TextMeshProUGUI objectNameText;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        HideInteractionCanvas(); // Asegurar que el panel empieza oculto
    }

    public void ShowInteractionCanvas(IsInteractable obj)
    {
        if (isUIOpen) return;

        if (interactionPanel == null || objectNameText == null) return;

        objectNameText.text = obj.currentObjectSO.objectName;
        interactionPanel.SetActive(true);
        isUIOpen = true;
    }

    public void HideInteractionCanvas()
    {
        if (interactionPanel != null)
            interactionPanel.SetActive(false);

        isUIOpen = false;
    }

    // Método para botón cerrar (X)
    public void OnCloseButtonPressed()
    {
        HideInteractionCanvas();
    }
}