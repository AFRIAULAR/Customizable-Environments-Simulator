using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerMobileController : MonoBehaviour
{
    public float speed = 5f;
    public MobileJoystick joystick;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector2 input = joystick.InputDirection;
        Vector3 move = new Vector3(input.x, 0, input.y);

        characterController.Move(move * speed * Time.deltaTime);
    }
}

