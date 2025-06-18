using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class MageController : MonoBehaviour
{
    public static MageController instance { get; private set; }
    private MageState currentState;


    private CharacterController characterController;
    Vector3 destination;

    public Vector3 Destination
    {
        get { return destination; }
        set
        { destination = value; }
    }


    public void Awake()
    {
        instance = this;
    }


    public void Init()
    {
        characterController = GetComponent<CharacterController>();
        destination = transform.position;

        if (characterController.isGrounded == false)
        {
            currentState = new MageStateFall();
        }
        else
        {
            currentState = new MageStateIdle();
        }
        currentState.Enter(instance);

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
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, GM.RAYCAST_DISTANCE))
        {
            Vector3 vector3 = hit.point - instance.transform.position;
            if (vector3.sqrMagnitude < GM.MIN_MOVE_SQR_DISTANCE)
            {
                return instance.transform.position;
            }
            else if (vector3.sqrMagnitude > GM.MAX_MOVE_SQR_DISTANCE)
            {
                return instance.transform.position + vector3.normalized * GM.MAX_MOVE_DISTANCE;
            }
            else return hit.point;
        }
        else
        {
            return instance.transform.position;
        }

    }

    private void GetInputAndChangeStage()
    {
        if ( (currentState is MageStateIdle) && Input.GetMouseButtonDown(1) )
        {
            IdleToRun();
        }

        if ( (currentState is MageStateIdle) && Input.GetKeyDown(KeyCode.Space) )
        {
            IdleToJump();
        }

        if ((currentState is MageStateRun) && Input.GetKeyDown(KeyCode.Space))
        {
            RunToJump();
        }


    }


    private void IdleToJump()
    { 
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
        if (currentState is not MageStateRun) 
        {
            currentState = new MageStateRun();
        }
        currentState.Enter(instance);
    }

    private void AutomaticChangeStage()
    {
        switch (currentState)
        {
            case MageStateRunJump: 
                currentState = new MageStateFall();
                break;

            default:
                currentState = new MageStateIdle();
                break; 
        }

        
    }

}

