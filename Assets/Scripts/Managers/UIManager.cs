using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public bool isUIOpen = false;

    private CameraController camController;
    private IsInteractable currentObj;

    [Header("UI Elements")]
    public GameObject interactionPanel;
    public TextMeshProUGUI objectNameText;

    [Header("Sliders Colors")]
    [SerializeField] private Slider RSlider, GSlider, BSlider;

    [Header("Sliders Text")]
    [SerializeField] private TMP_Text RValueText, GValueText, BValueText;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        camController = FindObjectOfType<CameraController>();
        HideInteractionCanvas();

        // Sliders
        RSlider.onValueChanged.AddListener(OnColorSliderChanged);
        GSlider.onValueChanged.AddListener(OnColorSliderChanged);
        BSlider.onValueChanged.AddListener(OnColorSliderChanged);
    }

    public void ShowInteractionCanvas(IsInteractable obj)
    {
        currentObj = obj;
        if (isUIOpen) return;
        if (interactionPanel == null || objectNameText == null) return;

        GameObject targetObject = null;
        if (obj.CompareTag("Wall"))
            targetObject = GameObject.Find("CombinedWalls");
        else if (obj.CompareTag("Floor"))
            targetObject = GameObject.Find("CombinedFloors");
        else
            targetObject = obj.gameObject;

        camController.isBlocked = true;

        objectNameText.text = obj.currentObjectSO.objectName;
        interactionPanel.SetActive(true);
        isUIOpen = true;

        Color currentColor = Color.white;

        if (targetObject != null)
        {
            Renderer rend = targetObject.GetComponent<Renderer>();
            if (rend != null)
                currentColor = rend.material.color;
        }

        RSlider.SetValueWithoutNotify(currentColor.r);
        GSlider.SetValueWithoutNotify(currentColor.g);
        BSlider.SetValueWithoutNotify(currentColor.b);

        RValueText.text = Mathf.RoundToInt(currentColor.r * 255).ToString();
        GValueText.text = Mathf.RoundToInt(currentColor.g * 255).ToString();
        BValueText.text = Mathf.RoundToInt(currentColor.b * 255).ToString();
    }

    public void HideInteractionCanvas()
    {
        if (interactionPanel != null)
            interactionPanel.SetActive(false);

        if (camController != null)
            camController.isBlocked = false;

        isUIOpen = false;
    }

    private void OnColorSliderChanged(float _)
    {
        SoundManager.instance.PlaySound(SoundType.SLIDER_MOVE, 0.3f);
        if (currentObj == null) return;

        float r = RSlider.value;
        float g = GSlider.value;
        float b = BSlider.value;

        RValueText.text = Mathf.RoundToInt(r * 255).ToString();
        GValueText.text = Mathf.RoundToInt(g * 255).ToString();
        BValueText.text = Mathf.RoundToInt(b * 255).ToString();

        Color newColor = new Color(r, g, b);

        if (currentObj.CompareTag("Wall"))
        {
            GameObject combinedWalls = GameObject.Find("CombinedWalls");
            if (combinedWalls != null)
                combinedWalls.GetComponent<Renderer>().material.color = newColor;
        }
        else if (currentObj.CompareTag("Floor"))
        {
            GameObject combinedFloors = GameObject.Find("CombinedFloors");
            if (combinedFloors != null)
                combinedFloors.GetComponent<Renderer>().material.color = newColor;
        }
        else
        {
            currentObj.SetColor(newColor);
        }
    }

    private Renderer GetTargetRenderer(IsInteractable obj)
    {
        if (obj.CompareTag("Wall"))
            return GameObject.Find("CombinedWalls")?.GetComponent<Renderer>();
        if (obj.CompareTag("Floor"))
            return GameObject.Find("CombinedFloors")?.GetComponent<Renderer>();
        return obj.GetComponent<Renderer>();
    }

    public void OnCloseButtonPressed()
    {
        SoundManager.instance.PlaySound(SoundType.BUTTON_CLOSE);
        HideInteractionCanvas();
    }
}
