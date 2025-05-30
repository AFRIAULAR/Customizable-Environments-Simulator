using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ObjectSelector : MonoBehaviour
{
    [Header("Double Tap Settings")]
    public float doubleTapMaxTime = 0.3f;
    public float doubleTapMaxDistance = 30f; // distancia máxima en píxeles entre los dos toques

    [Header("Interactable Layer")]
    public LayerMask interactableLayer;

    private float lastTapTime = 0f;
    private Vector2 lastTapPosition;

    void Update()
    {
        // Mobile
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            int fingerId = Input.GetTouch(0).fingerId;
            if (!EventSystem.current.IsPointerOverGameObject(fingerId))
            {
                DetectDoubleTap(Input.GetTouch(0).position);
            }
        }

        // PC
        if (Input.GetMouseButtonUp(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                DetectDoubleTap(Input.mousePosition);
            }
        }
    }

    void DetectDoubleTap(Vector2 currentPosition)
    {
        float currentTime = Time.time;

        if (currentTime - lastTapTime < doubleTapMaxTime &&
            Vector2.Distance(currentPosition, lastTapPosition) < doubleTapMaxDistance)
        {
            // Es un doble tap real?
            Ray ray = Camera.main.ScreenPointToRay(currentPosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, interactableLayer))
            {
                IsInteractable obj = hit.collider.GetComponent<IsInteractable>();
                if (obj != null)
                {
                    Debug.Log("Doble tap sobre objeto interactuable");
                    obj.Interact();
                }
            }

            lastTapTime = 0f; // reset para evitar multiples interact
        }
        else
        {
            // Primer tap tiempo-posicion
           lastTapTime = currentTime;
           lastTapPosition = currentPosition;
        }
    }
}