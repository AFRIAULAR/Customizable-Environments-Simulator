using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public MobileJoystick joystick;

    [Header("Camera Ref")]
    public Transform cameraTransform;

    public CharacterController characterController;

    void Start()
    {
       characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        // Keyboard
        float keyboardX = Input.GetAxis("Horizontal"); 
        float keyboardY = Input.GetAxis("Vertical"); 

        // Joystick
        Vector2 joystickInput = joystick.InputDirection;

        // First Joystick
        float inputX = joystickInput.x != 0 ? joystickInput.x : keyboardX;
        float inputY = joystickInput.y != 0 ? joystickInput.y : keyboardY;

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * inputY + right * inputX;
        characterController.Move(moveDirection * speed * Time.deltaTime);
    }
}

