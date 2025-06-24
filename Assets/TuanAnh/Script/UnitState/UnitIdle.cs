using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class UnitIdle : UnitState
{
    private bool needUpdateState = false;
    public override bool NeedUpdateState() => needUpdateState;
   
    private IUnitController controller;
    private CharacterController characterController;
    private Animator animator;
    

    

    public override void Enter(IUnitController InputController )
    {
        controller = InputController;
        //characterController = controller.GetCharacterController;
        //animator = controller.GetAnimator;
        needUpdateState = true;


    }

    public override void Update()
    {
        if (!needUpdateState) return;


    }

    public override void Exit()
    {
        needUpdateState = false ;
    }

    
}
