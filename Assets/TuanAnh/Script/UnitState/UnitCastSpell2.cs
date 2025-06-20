using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class UnitCastSpell2 : UnitState
{
    private bool needUpdateState = false;
    public override bool NeedUpdateState() => needUpdateState;
    IUnitController controller;
    Animator animator;


    public override void Enter(IUnitController controllerInput )
    {
        controller = controllerInput;
        needUpdateState = true;
        animator = controller.GetAnimator;

        animator.SetBool("Spell2", true);
    }

    public override void Update()
    {
        
        
    }

    public override void Exit()
    {
        needUpdateState = false;
        animator.SetBool("Spell1", false);
    }

   
}
