using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MobileJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler //EventSystem
{
    public Image joystickBackground;
    public Image joystickHandle;

    private Vector2 inputVector;

    public Vector2 InputDirection => inputVector;

    private void Start()
    {
        if (joystickBackground == null)
            joystickBackground = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBackground.rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out pos);

        pos.x = (pos.x / joystickBackground.rectTransform.sizeDelta.x);
        pos.y = (pos.y / joystickBackground.rectTransform.sizeDelta.y);

        inputVector = new Vector2(pos.x * 2, pos.y * 2);
        inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

        // Move joystick handle
        joystickHandle.rectTransform.anchoredPosition = new Vector2(
            inputVector.x * (joystickBackground.rectTransform.sizeDelta.x / 2),
            inputVector.y * (joystickBackground.rectTransform.sizeDelta.y / 2));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        joystickHandle.rectTransform.anchoredPosition = Vector2.zero;
    }
}

