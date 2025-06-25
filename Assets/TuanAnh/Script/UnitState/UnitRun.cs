using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class UnitRun : UnitState
{
    
    private bool needUpdateState = false;
    public override bool NeedUpdateState() => needUpdateState;

    private IUnitController controller;
    CharacterController characterController;
    Animator animator;

    private Vector3 runDestination;




    public override void Enter(IUnitController controllerInput, Vector3 destinationInput)
    {
        controller = controllerInput;
        characterController = controller.GetCharacterController;
        animator = controller.GetAnimator;

        runDestination = destinationInput;
        runDestination.y = 0;

        RotateCharacter();
        animator.SetBool("Run", true);
        needUpdateState = true;

    }

    public override void Update()
    {
        MoveCharacter();

    }


    public override void Exit()
    {
        animator.SetBool("Run", false);
        needUpdateState = false;
    }

    private void RotateCharacter()
    {
        Vector3 direction = runDestination - characterController.transform.position;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        characterController.transform.rotation = targetRotation;
    }

    private void MoveCharacter()
    {
        Vector3 direction = runDestination - characterController.transform.position;
        direction.y = 0;

        if (direction.sqrMagnitude < GM.MIN_MOVE_SQR_DISTANCE) 
        {   needUpdateState = false;
            return;
        }

        Vector3 oldPosition = characterController.transform.position;
        direction = direction.normalized * ( GM.Instance.GAME_SPEED * Time.deltaTime);

        //Xu ly neu dang chay ma bi roi xuong thap, thi ha do cao y : Luc nay khong phai nhay.
        if ( characterController.isGrounded == false ) 
        {
            direction.y = GM.GRAVITY * Time.deltaTime;
        }

        characterController.Move(direction);

        //Neu bi ket o doc cao do ham Move thi dung lai
        if ((characterController.transform.position - oldPosition).sqrMagnitude < GM.MIN_STUCK_DISTANCE)
        {
            needUpdateState = false;
            Debug.Log("Stuck on Terrance");
        }

    }

   

  
}
