using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;


public class MageController : MonoBehaviour , IUnitController
{
    public static MageController Instance { get; private set; }
    private UnitState currentState;
    private CharacterController characterController;
    public CharacterController GetCharacterController { get => characterController; }
   
    private Animator animator;
    public Animator GetAnimator { get => animator; }

    
    private bool notInAnimation = true;


    private Vector3 destination;
    private float towardDistance;


    public void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }


    public void Init()
    {
        
        destination = transform.position;

        if (characterController.isGrounded)
        {
            currentState = new UnitIdle();
        }
        else
        {
            currentState = new UnitFall();
        }
        currentState.Enter(Instance);

    }

    void Update()
    {
        GetInputAndChangeStage();

        if (currentState.NeedUpdateState())
            currentState.Update();
        else 
            BackToIdle();
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
                if (Input.GetMouseButtonDown(1) && notInAnimation )
                {   IdleToRun(); }

                if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space) && notInAnimation)
                {
                    towardDistance = GM.Instance.GAME_SPEED / 3;
                    ToJump();
                }
                else if (Input.GetKeyDown(KeyCode.Space) && notInAnimation)
                {
                    towardDistance = 0;
                    ToJump();
                }

                if (Input.GetKeyDown(KeyCode.Q) && notInAnimation)
                {
                    currentState.Exit();
                    currentState = new UnitCastSpell();
                    currentState.Enter(Instance);
                }

                break;

            case UnitRun:
                if (Input.GetMouseButtonDown(1) && notInAnimation )
                {
                    RunToRun();
                }
                else if (Input.GetKeyDown(KeyCode.Space) && notInAnimation)
                {
                    towardDistance = GM.Instance.GAME_SPEED/2;
                    ToJump();
                }
                break;

        }

    }


    private void ToJump()
    { 
        currentState.Exit();
        currentState = new UnitJump();
        currentState.Enter(Instance, towardDistance);   // Them bien Jump nhay ve toi dau
    }

    private void IdleToRun()
    {
        destination = GetDestination();
        if (destination == transform.position)
        {
            return;
        }
        
        currentState.Exit();
        currentState = new UnitRun();
        currentState.Enter(Instance, destination);  // Them bien chay den dau
    }

    private void RunToRun()
    {
        destination = GetDestination();
        if (destination == transform.position)
        {
            return;
        }
        currentState.Enter(Instance, destination);  // Them bien chay den dau
    }


    private void BackToIdle()
    {
        currentState.Exit();
        currentState = new UnitIdle();
        currentState.Enter(Instance);
                
    }

    // Goi o Animation Event
    public void AnimationTurnOnOff()
    {
        notInAnimation = !notInAnimation;
    }


}

