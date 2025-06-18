using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class MageStateFall : MageState
{
    private bool needUpdateState = false;
    private MageController mageController;
    private CharacterController characterController;
    private Animator animator;

    public override bool NeedUpdateState()
    {
        return needUpdateState ;
    }
    public override void Enter(MageController controller )
    {
        mageController = controller;
        characterController = mageController.GetComponent<CharacterController>();
        animator = mageController.GetComponent<Animator>();
        needUpdateState = true;

    }

    public override void Update()
    {
        if (needUpdateState)
        {
            Fall();
        }
    }

    public override void Exit()
    {
        animator.SetBool("fall", false);
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
            Vector3 gravity = new Vector3(0, GM.GRAVITY * Time.deltaTime, 0);
            characterController.Move(gravity);
            animator.SetBool("fall", true);
        }

    }

}
