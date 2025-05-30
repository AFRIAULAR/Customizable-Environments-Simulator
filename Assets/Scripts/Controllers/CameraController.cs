using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;  // player object
    public float rotationSpeed = 2f;
    
    public float zoomSpeed = 0.05f;
    public float minZoom = 2f;
    public float maxZoom = 10f;

    private float verticalRotation = 0f;
    private bool isTouching = false;
    private Vector2 lastTouchPosition;

    public bool isBlocked = false;
    void Update()
    {
        if (isBlocked) return;

        HandleRotation();
        HandleZoom();
    }

    void HandleRotation()
    {
        // Tocuh
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastTouchPosition = touch.position;
                isTouching = true;
            }
            else if (touch.phase == TouchPhase.Moved && isTouching)
            {
                Vector2 delta = touch.position - lastTouchPosition;
                lastTouchPosition = touch.position;

                float horizontal = delta.x * rotationSpeed * Time.deltaTime;
                player.Rotate(Vector3.up, horizontal);

                verticalRotation -= delta.y * rotationSpeed * Time.deltaTime;
                verticalRotation = Mathf.Clamp(verticalRotation, -45f, 45f);

                transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isTouching = false;
            }
        }
        else // Mouse
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            player.Rotate(Vector3.up, mouseX * rotationSpeed);

            verticalRotation -= mouseY * rotationSpeed;
            verticalRotation = Mathf.Clamp(verticalRotation, -45f, 45f);

            transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }
    }

    void HandleZoom()
    {
        // Pinch
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 prevTouch0 = touch0.position - touch0.deltaPosition;
            Vector2 prevTouch1 = touch1.position - touch1.deltaPosition;

            float prevMagnitude = (prevTouch0 - prevTouch1).magnitude;
            float currentMagnitude = (touch0.position - touch1.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Vector3 cameraPosition = transform.localPosition;
            cameraPosition.z += difference * zoomSpeed;
            cameraPosition.z = Mathf.Clamp(cameraPosition.z, -maxZoom, -minZoom);
            transform.localPosition = cameraPosition;
        }
        else
        {
            // Scroll
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0f)
            {
                Vector3 cameraPosition = transform.localPosition;
                cameraPosition.z += scroll * zoomSpeed * 100f;
                cameraPosition.z = Mathf.Clamp(cameraPosition.z, -maxZoom, -minZoom);
                transform.localPosition = cameraPosition;
            }
        }
    }

}
