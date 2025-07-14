using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour , IUnitController
{
    public static PlayerController Instance { get; private set; }
    private UnitState currentState;
    private CharacterController characterController;
    public CharacterController GetCharacterController { get => characterController; }
   

    private Animator animator;
    public Animator GetAnimator { get => animator; }
    [SerializeField] GameObject magicBallPrefab;

    private bool notInAnimating = true;
    public bool NotInAnimating
    {
        get => notInAnimating;
        set => notInAnimating = value;
    }

    private Vector3 destination;
    private float towardDistance;

    [SerializeField] private PlayerData inputData;
    private PlayerData currentData;
    public PlayerData CurrentPlayerData
    {
        get => currentData;
        set => currentData = value;
    }


    public event Action<float> PlayerOnHealthChanged;



    public void Awake()
    {
        Instance = this;
        
    }


    public void Init()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        currentData = inputData.CloneData();
        SetEnterState();


    }

    void Update()
    {

        GetInputAndChangeStage();

        if (currentState.NeedUpdateState())
            currentState.Update();
        else 
            BackToIdle();
    }


    private void SetEnterState()
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

    private void HandleRotatePlayer()
    {
        float rotateInput = Input.GetAxis("Mouse X");
        if (Mathf.Abs(rotateInput) > 0.01f)
        {
            transform.Rotate(0, rotateInput * GMData.Instance.GAME_SPEED, 0);
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
            else if (vector3.sqrMagnitude > GMData.Instance.MAX_MOVE_SQR_DISTANCE)
            {
                return Instance.transform.position + vector3.normalized * GMData.Instance.MAX_MOVE_SQR_DISTANCE;
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
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                { 
                    HandleRotatePlayer(); 
                }

                if ( (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKeyDown(KeyCode.Space) && notInAnimating)
                {
                    towardDistance = GMData.Instance.GAME_SPEED;
                    ToJump();
                }
                else if (Input.GetKeyDown(KeyCode.Space) && notInAnimating)
                {
                    towardDistance = 0;
                    ToJump();
                }
                else if (Input.GetMouseButtonDown(1) && notInAnimating)
                {
                    IdleToRun();
                }
                else if (Input.GetKeyDown(KeyCode.Q) && notInAnimating)
                {
                    IdleToCastSpell1();
                }
                else if (Input.GetKeyDown(KeyCode.W) && notInAnimating)
                {
                    IdleToCastSpell2();
                }
                break;

            case UnitRun:
                if (Input.GetMouseButtonDown(1) && notInAnimating )
                {
                    RunToRun();
                }
                else if (Input.GetKeyDown(KeyCode.Space) && notInAnimating)
                {
                    towardDistance = GMData.Instance.GAME_SPEED;
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

    private void IdleToCastSpell1()
    {
        currentState.Exit();
        currentState = new UnitCastSpell1();
        destination = GetDestinationForSpell(GMData.Instance.RAYCAST_DISTANCE);
        currentState.Enter(Instance, destination, magicBallPrefab);
    }

    private void IdleToCastSpell2()
    {
        currentState.Exit();
        currentState = new UnitCastSpell2();
        currentState.Enter(Instance);
    }

    private void BackToIdle()
    {
        currentState.Exit();
        currentState = new UnitIdle();
        currentState.Enter(Instance);
                
    }

    // Goi o Animation Event
    public void FlagInOutAnimating()
    {
        notInAnimating = !notInAnimating;
    }

    public void TakeDamage(float damage)
    {
        currentData.currentHealth -= damage;
        PlayerOnHealthChanged?.Invoke(currentData.currentHealth); // ? co nguoi nghe ms goi
        if (currentData.currentHealth <= 0)
        {
            currentData.currentHealth = 0;
            GM.Instance.GameOver();
            Debug.Log("Player has died.");
        }

    }


}

