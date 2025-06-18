using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class MageStateRun : MageState
{
    private MageController mageController;
    CharacterController characterController;
    private Vector3 runDestination;
    private bool needUpdateState = false;

    public override bool NeedUpdateState()
    { return needUpdateState;
    }


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
        animator.SetBool("run", true);
        needUpdateState = true;
       
        lineRenderer = mageController.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, mageController.transform.position);
    }

    public override void Update()
    {
        if ( needUpdateState )
        {
            MoveCharacter();

            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, mageController.transform.position);
        }

    }


    public override void Exit()
    {
        animator.SetBool("run", false);
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

        // Kiem tra neu da den dich
        if (direction.sqrMagnitude < GM.MIN_MOVE_SQR_DISTANCE)
        {
            Debug.Log("Reached destination: " + runDestination);
            needUpdateState = false;
            return;
        }

     
        Vector3 oldPosition = mageController.transform.position;
        direction = direction.normalized * GM.instance.SpeedGame * Time.deltaTime;
        if (characterController.isGrounded == false)
        {
            direction.y = GM.GRAVITY * Time.deltaTime; 
        }
        characterController.Move(direction);

        
        // Neu bi ket o doc cao do ham Move thi dung lai
        if ((mageController.transform.position - oldPosition).sqrMagnitude < GM.MIN_STUCK_DISTANCE)
        {
            needUpdateState = false;
            Debug.Log("Stuck at: " + mageController.transform.position);
        }

    }

    



}
