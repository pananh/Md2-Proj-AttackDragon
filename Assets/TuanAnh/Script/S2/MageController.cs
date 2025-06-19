using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class MageController : MonoBehaviour
{
    public static MageController Instance { get; private set; }
    private UnitState currentState;


    private CharacterController characterController;
    private Vector3 destination;
    private float jumpForwardSpeed;

    public Vector3 Destination
    {
        get { return destination; }
        set
        { destination = value; }
    }
    public float JumpForwardSpeed
    {
        get { return jumpForwardSpeed; }
        set { jumpForwardSpeed = value; }
    }    


    public void Awake()
    {
        Instance = this;
    }


    public void Init()
    {
        characterController = GetComponent<CharacterController>();
        destination = transform.position;

        if (characterController.isGrounded)
        {
            currentState = new UnitIdle();
        }
        else
        {
            currentState = new UnitFall();
            Debug.Log(" 1 ");
        }
        currentState.Enter(Instance);

    }

    void Update()
    {

        GetInputAndChangeStage();

        if (currentState.NeedUpdateState())
        {
            currentState.Update();
        }
        else
        {
            currentState.Exit();
            AutomaticChangeStage();
        }



    }

    private static Vector3 GetDestination()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, GM.RAYCAST_DISTANCE))
        {
            Vector3 vector3 = hit.point - Instance.transform.position;
            if (vector3.sqrMagnitude < GM.MIN_MOVE_SQR_DISTANCE)
            {
                return Instance.transform.position;
            }
            else if (vector3.sqrMagnitude > GM.MAX_MOVE_SQR_DISTANCE)
            {
                return Instance.transform.position + vector3.normalized * GM.MAX_MOVE_DISTANCE;
            }
            else return hit.point;
        }
        else
        {
            return Instance.transform.position;
        }

    }

    private void GetInputAndChangeStage()
    {
        switch (currentState)
        {
            case UnitIdle:
                if (Input.GetMouseButtonDown(1))
                {   IdleToRun(); }

                if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space))
                {
                    jumpForwardSpeed = GM.Instance.GAME_SPEED / 3;
                    IdleToJump();
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    jumpForwardSpeed = 0;
                    IdleToJump();
                }
                break;

            case UnitRun:
                if (Input.GetMouseButtonDown(1))
                {
                    RunToRun();
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    RunToJump();
                }

                break;

        }

      

    }


    private void IdleToJump()
    { 
        currentState.Exit();
        currentState = new UnitIdleJump();
        currentState.Enter(Instance);

        
    }

    private void RunToJump()
    {
    }

    private void IdleToRun()
    {
        if (destination == transform.position)
        {
            return;
        }
        destination = GetDestination();
        currentState.Exit();
        currentState = new UnitRun();
        currentState.Enter(Instance);
    }

    private void RunToRun()
    {
        if (destination == transform.position)
        {
            return;
        }
        destination = GetDestination();
        currentState.Exit();
        currentState.Enter(Instance);
    }


    private void AutomaticChangeStage()
    {
        switch (currentState)
        {
            case UnitRunJump: 
                currentState = new UnitFall();
                break;

            default:
                currentState = new UnitIdle();
                Debug.Log(" x1 ");

                // De tam thoi
                currentState.Enter(Instance);


                break; 
        }

        
    }

}

