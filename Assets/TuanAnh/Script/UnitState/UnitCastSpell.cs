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
    private bool isInstated;
    private int isCastingStage; 


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
        isCastingStage = 0;
    }

    public override void Update()
    {
        if (!isInstated)
        {
            SpawnFireBall();
            isInstated = true;
        }

        if ( (isCastingStage == 0) && !controller.NotInFixAnimation)
        {
            isCastingStage = 1;
        }
        else if (isCastingStage == 1 && controller.NotInFixAnimation)
        {
            magicBall.NeedMoving = true;
            isCastingStage = 2;
        }
        else if (isCastingStage == 2 && !controller.NotInFixAnimation)
        {
            isCastingStage = 3;
        }
        else if (isCastingStage == 3 && controller.NotInFixAnimation)
        {
            needUpdateState = false;
        }

    }

    public override void Exit()
    {
        needUpdateState = false;
        animator.SetBool("Spell", false);
    }

    private void SpawnFireBall()
    {
        Vector3 spawnPosition = characterController.transform.TransformPoint(GM.MAGIC_BALL_LOCAL_OFFSET);
        Quaternion spawnRotation = Quaternion.LookRotation(targetPosition - spawnPosition);
        
        magicBallObject = GameObject.Instantiate(magicBallPrefab, spawnPosition, spawnRotation);
        magicBall = magicBallObject.GetComponent<MagicBall>();
        magicBall.Init(GM.Instance.GAME_SPEED, GM.Instance.GAME_SPEED*2, targetPosition);

    }




}
