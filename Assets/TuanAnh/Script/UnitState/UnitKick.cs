using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class UnitKick : UnitState
{
    private bool needUpdateState = false;
    public override bool NeedUpdateState() => needUpdateState;

    private CharacterController characterController;
    private IUnitController controller;
    private Animator animator;

    

    public override void Enter(IUnitController InputController)
    {
        controller = InputController;
        characterController = controller.GetCharacterController;
        animator = controller.GetAnimator;


        needUpdateState = true;
        animator.SetBool("Kick", true);
    }

    public override void Update()
    {
        
        
    }

    public override void Exit()
    {
        needUpdateState = false;
        animator.SetBool("Kick", false);
    }

   
}
