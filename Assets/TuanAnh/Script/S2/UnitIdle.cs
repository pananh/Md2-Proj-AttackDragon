using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class UnitIdle : UnitState
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
        animator.SetBool("Idle", true);
    }

    public override void Update()
    {
        //if (needUpdateState)
        //{
           
        //}


    }

    public override void Exit()
    {
        animator.SetBool("Idle", false);
    }

    
}
