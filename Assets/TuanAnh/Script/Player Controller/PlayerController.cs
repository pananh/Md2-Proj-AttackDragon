using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    

    CharacterController characterController;
    public float moveSpeed = 10f, rotationSpeed = 5f, gravity = -9.81f, jumpSpeed = 8f;
    private float rotationY;


    InputAction moveAction, lookAction;


    void Start()
    {
        characterController = GetComponent<CharacterController>();

        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");

    }

    private void Update()
    {
        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        Debug.Log(moveVector);
        Move(moveVector);

        Vector2 lookVector = lookAction.ReadValue<Vector2>();
        Debug.Log(lookVector);
        Rotate(lookVector);

    }




    public void Move(Vector2 moveVector)
    {
        Vector3 move = transform.forward * moveVector.y + transform.right * moveVector.x;
        move *= moveSpeed * Time.deltaTime;
        characterController.Move(move);
    }

    public void Rotate(Vector2 lookVector)
    {
        rotationY += lookVector.x * rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, rotationY, 0);
    }

}
