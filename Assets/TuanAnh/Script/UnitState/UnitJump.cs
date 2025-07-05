using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class UnitJump : UnitState
{
    private bool needUpdateState = false;
    public override bool NeedUpdateState() => needUpdateState;
    private IUnitController controller;
    
    private CharacterController characterController;
    private Animator animator;
    float verticalVelocity, horizontalVelocity;

   

    public override void Enter(IUnitController controllerInput, float towardDistance )
    {
        controller = controllerInput;
        characterController = controller.GetCharacterController;
        animator = controller.GetAnimator;
        needUpdateState = true;
        verticalVelocity = GMData.Instance.GAME_SPEED/2;
        horizontalVelocity = towardDistance;
        animator.SetBool("Jump", true);
        animator.SetBool("InAir", true);
    }

    public override void Update()
    {
        JumpCharacter();

    }

    public override void Exit()
    {
        animator.SetBool("Jump", false);
        animator.SetBool("InAir", false);
        needUpdateState = false;
    }


    private void JumpCharacter()
    {
        verticalVelocity += GMData.Instance.GRAVITY * Time.deltaTime;

        Vector3 jumpMove = characterController.transform.forward * (horizontalVelocity * Time.deltaTime) +
                        Vector3.up * (verticalVelocity * Time.deltaTime);

        characterController.Move(jumpMove);
        if (characterController.isGrounded)
        {
            needUpdateState = false;
        }

    }



}
