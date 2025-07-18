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
    public GameObject GetGameObject { get; set; }

    private Animator animator;
    public Animator GetAnimator { get => animator; }
    [SerializeField] GameObject magicBallPrefab1;
    [SerializeField] GameObject magicBallPrefab2;

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
    private bool isDead;
    public bool IsDead
    {
        get => isDead;
        set => isDead = value;
    }


    public event Action< PlayerData > EvPlayerDataChanged;
    public event Action EvPlayerStartRun;
    public event Action EvPlayerStopRun;
    public event Action EvPlayerCastSpell1;
    public event Action EvPlayerCastSpell2;
    public event Action EvPlayerJump;
    public event Action EvPlayerLand;
    public event Action EvPlayerDie;

    public void Awake()
    {
        Instance = this;
        GetGameObject = this.gameObject;
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
        if (isDead)
        {
            return; 
        }
        GetInputAndChangeStage();

        if (currentState.NeedUpdateState())
            currentState.Update();
        else 
            BackToIdle();
    }


    private void SetEnterState()
    {
        isDead = false;
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
                    ToCastSpell1();
                }
                else if (Input.GetKeyDown(KeyCode.W) && notInAnimating)
                {
                    ToCastSpell2();
                }
                break;

            case UnitRun:
                if (Input.GetMouseButtonDown(1) && notInAnimating )
                {
                    RunToRun();
                }
                else if (Input.GetKeyDown(KeyCode.Q) && notInAnimating)
                {
                    ToCastSpell1();
                    
                }
                else if (Input.GetKeyDown(KeyCode.W) && notInAnimating)
                {
                    ToCastSpell2();
                    
                }
                else if (Input.GetKeyDown(KeyCode.Space) && notInAnimating)
                {
                    towardDistance = GMData.Instance.GAME_SPEED;
                    EvPlayerStopRun?.Invoke();
                    ToJump();
                }
                break;
        }

    }


    private void ToJump()
    {
        if (currentState is UnitRun)
        {
            EvPlayerStopRun?.Invoke();
        }
        currentState.Exit();
        currentState = new UnitJump();
        EvPlayerJump?.Invoke();
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
        EvPlayerStartRun?.Invoke();
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

    private void ToCastSpell1()
    {
        if (currentState is UnitRun)
        {
            EvPlayerStopRun?.Invoke();
        }
        
        currentState.Exit();
        EvPlayerCastSpell1?.Invoke();
        currentState = new UnitCastSpell1();
        currentState.Enter(Instance, magicBallPrefab1);
    }

    private void ToCastSpell2()
    {
        if (currentState is UnitRun)
        {
            EvPlayerStopRun?.Invoke();
        }
        currentState.Exit();
        EvPlayerCastSpell2?.Invoke();
        currentState = new UnitCastSpell2();
        currentState.Enter(Instance, magicBallPrefab2);
    }

    private void BackToIdle()
    {
        if (currentState is UnitRun)
        {
            EvPlayerStopRun?.Invoke();
        }
        if (currentState is UnitJump)
        {
            EvPlayerLand?.Invoke();
        }
        if (currentState is UnitIdle)
        {
            return; 
        }
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
        EvPlayerDataChanged?.Invoke(currentData); // ? co nguoi nghe ms goi
        if (currentData.currentHealth <= 0)
        {
            currentData.currentHealth = 0;
            isDead = true;
            EvPlayerDie?.Invoke();
            PlayerDie();
        }
    }

    public void PlayerDie()
    {
        currentState.Exit();
        currentState = new UnitDie();
        currentState.Enter(Instance);
        GM.Instance.GameOver();
        Debug.Log("Player has died.");

    }


    public void TakeExperience(float exp)
    {
        currentData.exp += exp;
        if (currentData.exp >= currentData.expNextLevel)
            LevelUp();
        EvPlayerDataChanged?.Invoke(currentData); // ? co nguoi nghe ms goi
    }

    private void LevelUp()
    {
        currentData.LevelUp(currentData);
        Debug.Log($"Player leveled up to level {currentData.level}!");
    }

    public void ResetInstance()
    {
        Instance = null;
    }

}

