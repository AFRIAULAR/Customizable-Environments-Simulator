using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    [Header("Detect Double Tap")]
    public float doubleTapMaxTime = 0.3f;

    [Header("Interactable Layer")]
    public LayerMask interactableLayer;
    private float lastTapTime = 0f;

    void Update()
    {
        // Mobile
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            DetectDoubleTap(Input.GetTouch(0).position);
        }

        // PC
        if (Input.GetMouseButtonUp(0))
        {
            DetectDoubleTap(Input.mousePosition);
        }
    }

    void DetectDoubleTap(Vector2 screenPos)
    {
        float currentTime = Time.time;

        if (currentTime - lastTapTime < doubleTapMaxTime)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, interactableLayer))
            {
                IsInteractable obj = hit.collider.GetComponent<IsInteractable>();
                if (obj != null)
                {
                    Debug.Log("¡Doble tap sobre objeto interactuable!");
                    obj.Interact();
                }


            }
        }

        lastTapTime = currentTime;
    }
}