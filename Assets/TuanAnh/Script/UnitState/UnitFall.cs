using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class UnitFall : UnitState
{
    private bool needUpdateState = false;
    public override bool NeedUpdateState() => needUpdateState;

    private IUnitController controller;
    private CharacterController characterController;
    private Animator animator;


    public override void Enter(IUnitController controllerInput )
    {
        controller = controllerInput;
        characterController = controller.GetCharacterController;
        animator = controller.GetAnimator;
        needUpdateState = true;
        animator.SetBool("Fall", true);
        animator.SetBool("InAir", true);
    }

    public override void Update()
    {
        Fall();
        
    }

    public override void Exit()
    {
        animator.SetBool("Fall", false);
        animator.SetBool("InAir", false);
        needUpdateState = false;
    }

    private void Fall()
    {
        if (characterController.isGrounded)
        {
            needUpdateState = false;
        }
        else
        {
            Vector3 gravityVector = Vector3.up * (GMData.Instance.GRAVITY * Time.deltaTime); 
            characterController.Move(gravityVector);
        }

    }

}
