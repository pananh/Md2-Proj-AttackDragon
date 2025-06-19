using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class UnitFall : UnitState
{
    private bool needUpdateState = false;
    private MageController mageController;
    private CharacterController characterController;
    private Animator animator;

    public override bool NeedUpdateState() => needUpdateState;

    public override void Enter(MageController controller )
    {
        mageController = controller;
        characterController = mageController.GetComponent<CharacterController>();
        animator = mageController.GetComponent<Animator>();
        needUpdateState = true;

    }

    public override void Update()
    {
        Fall();
        
    }

    public override void Exit()
    {
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
            Vector3 gravityVector = Vector3.up * (GM.GRAVITY * Time.deltaTime); 
            characterController.Move(gravityVector);
            animator.SetBool("InAir", true);
        }

    }

}
