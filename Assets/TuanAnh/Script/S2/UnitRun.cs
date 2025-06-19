using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class UnitRun : UnitState
{
    private MageController mageController;
    CharacterController characterController;
    private Vector3 runDestination;
    private bool needUpdateState = false;
    private float verticalVelocityForJump;
    private bool isJumpedInRunning;

    public override bool NeedUpdateState() => needUpdateState;


    LineRenderer lineRenderer;
    Animator animator;

    public override void Enter(MageController controller)
    {
        mageController = controller;
        characterController = mageController.GetComponent<CharacterController>();
        animator = mageController.GetComponent<Animator>();

        runDestination = mageController.Destination;
        runDestination.y = 0;

        RotateCharacter();
        animator.SetBool("Run", true);
        needUpdateState = true;
        verticalVelocityForJump = 0;
        isJumpedInRunning = false;

        lineRenderer = mageController.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, mageController.transform.position);
    }

    public override void Update()
    {
        MoveCharacter();

        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, mageController.transform.position);

    }


    public override void Exit()
    {
        animator.SetBool("Run", false);
    }

    private void RotateCharacter()
    {
        Vector3 direction = runDestination - mageController.transform.position;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        mageController.transform.rotation = targetRotation;
    }

    private void MoveCharacter()
    {
        Vector3 direction = runDestination - mageController.transform.position;
        direction.y = 0;
        if (direction.sqrMagnitude < GM.MIN_MOVE_SQR_DISTANCE)
        {
            needUpdateState = false;
            return;
        }

        ProcessJumpInRunning();
        
        Vector3 oldPosition = mageController.transform.position;
        direction = direction.normalized * ( GM.Instance.GAME_SPEED * Time.deltaTime);

        //Xu ly neu dang chay ma bi roi xuong thap, thi ha do cao y : Luc nay khong phai nhay.
        if ( (!isJumpedInRunning) && (characterController.isGrounded == false)) 
        {
            direction.y = GM.GRAVITY * Time.deltaTime; 
        }

        if ( (isJumpedInRunning && characterController.isGrounded == false) )
        {
            verticalVelocityForJump += GM.GRAVITY*Time.deltaTime;
            direction.y = verticalVelocityForJump * Time.deltaTime;
        }
        
        
        characterController.Move(direction);


        // Xu ly khi cham dat
        if (characterController.isGrounded)
        {
            isJumpedInRunning = false;
            animator.SetBool("InAir", false);
            animator.SetBool("Jump",false);
        }

        
        // Neu bi ket o doc cao do ham Move thi dung lai
        if ((mageController.transform.position - oldPosition).sqrMagnitude < GM.MIN_STUCK_DISTANCE)
        {
            needUpdateState = false;
            Debug.Log("Stuck at: " + mageController.transform.position);
        }

    }

    private void ProcessJumpInRunning()
    {
        if (Input.GetKeyDown (KeyCode.Space) && (!isJumpedInRunning) && (characterController.isGrounded) )
        {
            verticalVelocityForJump = GM.Instance.GAME_SPEED / 2;
            animator.SetBool("Jump", true);
            animator.SetBool("InAir", true);
            isJumpedInRunning = true;
        }    
    }


  
}
