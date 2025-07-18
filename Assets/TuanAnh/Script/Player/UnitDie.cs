using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class UnitDie : UnitState
{
    private bool needUpdateState = false;
    public override bool NeedUpdateState() => needUpdateState;
   
    private IUnitController controller;
    private CharacterController characterController;
    private Animator animator;
    

    

    public override void Enter(IUnitController InputController )
    {
        UnityEngine.Debug.Log("Unit is dying");

        controller = InputController;
        controller.GetAnimator.SetBool("Die", true);

        CharacterController characterController = controller.GetCharacterController;
        characterController.enabled = false;

        Rigidbody rigidbody = controller.GetGameObject.GetComponent<Rigidbody>();
        if (rigidbody == null)
        {
            UnityEngine.Debug.Log("Rigidbody");
            rigidbody = controller.GetGameObject.AddComponent<Rigidbody>();
        }
        rigidbody.isKinematic = true;
        rigidbody.useGravity = true;





    }

    public override void Exit()
    {
       
    }

    
}
