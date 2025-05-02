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

    private Vector2 lastTouchPosition;
    private bool isTouching = false;

    void Update()
    {
        HandleMovement();
        HandleCameraRotation();
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
}
