using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHanle : MonoBehaviour
{

    public PlayerController playerController;
    InputAction moveAction, lookAction;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        Cursor.visible = false;

    }

    void Update()
    {
        Vector2 moveVector = moveAction.ReadValue<Vector2>();
        if (moveVector.sqrMagnitude < GM.MIN_MOVE_SQR_DISTANCE)
            return; 
        Debug.Log(moveVector);
        playerController.Move(moveVector);

        //Vector2 lookVector = lookAction.ReadValue<Vector2>();
        //Debug.Log(lookVector);
        //playerController.Rotate(lookVector);
    }
}
