using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class UnitCastSpell : UnitState
{
    private bool needUpdateState = false;
    public override bool NeedUpdateState() => needUpdateState;
    IUnitController controller;
    CharacterController characterController;    
    Animator animator;

    GameObject magicBallPrefab;
    GameObject magicBallObject;
    MagicBall magicBall;

    private Vector3 targetPosition;
    private bool isInstated = false;


    public override void Enter(IUnitController controllerInput, Vector3 targetPosInput, GameObject magicBallPrefabInput)
    {
        controller = controllerInput;
        needUpdateState = true;
        animator = controller.GetAnimator;
        characterController = controller.GetCharacterController;

        targetPosition = targetPosInput;
        magicBallPrefab = magicBallPrefabInput;
        animator.SetBool("Spell", true);
        isInstated = false;

        Debug.Log("In Cast Spell: " + controller.NotInFixAnimation);
    }

    public override void Update()
    {
        if (!isInstated)
        {
            SpawnFireBall();
            isInstated = true;
        }

        ////Debug.Log("Cast Spell State: " + magicBall.NeedMoving);
        //if ( controller.NotInFixAnimation )
        //{
        //    //Debug.Log("Cast Spell State False: " + controller.NotInFixAnimation);
        //    magicBall.NeedMoving = true;
        //    //Debug.Log("Cast Spell State True: " + magicBall.NeedMoving);
        //}

    }

    public override void Exit()
    {
        needUpdateState = false;
        animator.SetBool("Spell",false);

    }

    private void SpawnFireBall()
    {
        Vector3 localOffset = new Vector3(0, 1, 2); 
        Vector3 spawnPosition = characterController.transform.TransformPoint(localOffset);
        Quaternion spawnRotation = Quaternion.LookRotation(targetPosition - spawnPosition);
        
        magicBallObject = GameObject.Instantiate(magicBallPrefab, spawnPosition, spawnRotation);
        magicBall = magicBallObject.GetComponent<MagicBall>();
        magicBall.Init(2f, 5f, targetPosition);

    }




}
