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
    [SerializeField] GameObject magicBallPrefab;

    private bool notInFixAnimation = true;
    public bool NotInFixAnimation
    {
        get => notInFixAnimation;
        set => notInFixAnimation = value;
    }

    private Vector3 destination;
    private float towardDistance;




    [SerializeField] private PlayerData playerData;
    private float health;




    public void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        health = playerData.health;

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

        HandleRotatePlayer();

        if (currentState.NeedUpdateState())
            currentState.Update();
        else 
            BackToIdle();
    }

    private void HandleRotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftShift) && (currentState is UnitIdle))
        {
            float rotateInput = Input.GetAxis("Mouse X");
            if (Mathf.Abs(rotateInput) > 0.01f)
            {
                transform.Rotate(0, rotateInput * GMData.Instance.GAME_SPEED, 0);
            }
           
        }

    }

    private static Vector3 GetDestinationForMove()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, GMData.Instance.RAYCAST_DISTANCE))
        {
            Vector3 vector3 = hit.point - Instance.transform.position;
            if (vector3.sqrMagnitude < GMData.Instance.MIN_MOVE_SQR_DISTANCE)
            {
                return Instance.transform.position;
            }
            else if (vector3.sqrMagnitude > GMData.Instance.MIN_MOVE_SQR_DISTANCE)
            {
                return Instance.transform.position + vector3.normalized * GMData.Instance.MIN_MOVE_SQR_DISTANCE;
            }
            else return hit.point;
        }
        else
        {
            return Instance.transform.position;
        }
    }
    private static Vector3 GetDestinationForSpell(float distance)
    {
        return Instance.transform.position + Instance.transform.forward * distance;
    }

    private void GetInputAndChangeStage()
    {
        switch (currentState)
        {
            case UnitIdle:
                if (Input.GetMouseButtonDown(1) && notInFixAnimation )
                {   IdleToRun(); }

                if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space) && notInFixAnimation)
                {
                    towardDistance = GMData.Instance.GAME_SPEED / 3;
                    ToJump();
                }
                else if (Input.GetKeyDown(KeyCode.Space) && notInFixAnimation)
                {
                    towardDistance = 0;
                    ToJump();
                }

                if (Input.GetKeyDown(KeyCode.Q) && notInFixAnimation)
                {
                    IdleToCastSpell();
                }

                break;

            case UnitRun:
                if (Input.GetMouseButtonDown(1) && notInFixAnimation )
                {
                    RunToRun();
                }
                else if (Input.GetKeyDown(KeyCode.Space) && notInFixAnimation)
                {
                    towardDistance = GMData.Instance.GAME_SPEED/2;
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
        destination = GetDestinationForMove();
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
        destination = GetDestinationForMove();
        if (destination == transform.position)
        {
            return;
        }
        currentState.Enter(Instance, destination);  // Them bien chay den dau
    }

    private void IdleToCastSpell()
    {
        currentState.Exit();
        currentState = new UnitCastSpell();
        destination = GetDestinationForSpell(GMData.Instance.RAYCAST_DISTANCE);
        currentState.Enter(Instance, destination, magicBallPrefab);
    }

    private void BackToIdle()
    {
        currentState.Exit();
        currentState = new UnitIdle();
        currentState.Enter(Instance);
                
    }

    // Goi o Animation Event
    public void CheckInFixedAnimation()
    {
        //Debug.Log(" CheckInFixedAnimation: " + notInFixAnimation);  
        notInFixAnimation = !notInFixAnimation;
    }

    public void TakeDamage(int damage)
    {
       
        Debug.Log($"MageController took {damage} damage.");
    }

}

