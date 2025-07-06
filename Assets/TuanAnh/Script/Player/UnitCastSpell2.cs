using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class UnitCastSpell2 : UnitState
{
    private bool needUpdateState = false;
    public override bool NeedUpdateState() => needUpdateState;

    private CharacterController characterController;
    private IUnitController controller;
    private Animator animator;

    private float  punchDuration = 3.5f; // Duration of the punch animation


    public override void Enter(IUnitController InputController)
    {
        controller = InputController;
        characterController = controller.GetCharacterController;
        animator = controller.GetAnimator;


        needUpdateState = true;
        animator.SetBool("Spell2", true);
    }

    public override void Update()
    {
        punchDuration -= Time.deltaTime;
        if (punchDuration <= 0f)
        {
            needUpdateState = false; // End the punch state after the duration
        }
       

    }

    public override void Exit()
    {
        needUpdateState = false;
        animator.SetBool("Spell2", false);
    }

   
}
