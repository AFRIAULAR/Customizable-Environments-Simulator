using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Character Movement")]
    public float speed = 5f;
    public CharacterController characterController;

    [Header("Camera Movement")]
    public Transform cameraTransform;
    public float mouseSensitivityX = 2f;
    public float mouseSensitivityY = 2f;
    private float verticalRotation = 0f;

    [Header("Camera Zoom")]
    public float zoomSpeed = 0.05f;
    public float minZoom = 2f;
    public float maxZoom = 10f;

    private Vector2 lastTouchPosition;
    private bool isTouching = false;

    void Update()
    {
        HandleMovement();
        HandleCameraRotation();
        HandleCameraZoom(); 
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * moveX + transform.forward * moveZ) * speed * Time.deltaTime;
        characterController.Move(move);
    }

    void HandleCameraRotation()
    {
        if (Input.touchSupported && Input.touchCount > 0)
        {
            HandleTouchRotation();
        }
        else
        {
            HandleMouseRotation();
        }
    }

    void HandleMouseRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY;
        ApplyRotation(mouseX, mouseY);
    }

    void HandleTouchRotation()
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            lastTouchPosition = touch.position;
            isTouching = true;
        }
        else if (touch.phase == TouchPhase.Moved && isTouching)
        {
            Vector2 delta = touch.deltaPosition;
            float deltaX = delta.x * mouseSensitivityX * 0.1f;
            float deltaY = delta.y * mouseSensitivityY * 0.1f;
            ApplyRotation(deltaX, deltaY);
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            isTouching = false;
        }
    }

    void ApplyRotation(float horizontal, float vertical)
    {
        verticalRotation -= vertical;
        verticalRotation = Mathf.Clamp(verticalRotation, -45f, 45f);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * horizontal);
    }

    void HandleCameraZoom()
    {
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 prevTouch0 = touch0.position - touch0.deltaPosition;
            Vector2 prevTouch1 = touch1.position - touch1.deltaPosition;

            float prevMagnitude = (prevTouch0 - prevTouch1).magnitude;
            float currentMagnitude = (touch0.position - touch1.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Vector3 cameraPosition = cameraTransform.localPosition;
            cameraPosition.z += difference * zoomSpeed;
            cameraPosition.z = Mathf.Clamp(cameraPosition.z, -maxZoom, -minZoom);
            cameraTransform.localPosition = cameraPosition;
        }
    }
}
