using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

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

        UnityEngine.Debug.Log("2");

    }

    public override void Update()
    {
        if (needUpdateState)
        {
            UnityEngine.Debug.Log("3");
        }


    }

    public override void Exit()
    {
        UnityEngine.Debug.Log("4");
        animator.SetBool("Idle", false);
    }

    
}
