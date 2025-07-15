using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class UnitCastSpell1 : UnitState
{
    private bool needUpdateState = false;
    public override bool NeedUpdateState() => needUpdateState;
    private IUnitController controller;
    private CharacterController characterController;    
    private Animator animator;

    GameObject magicBallPrefab;
    private List <MagicBall> magicBallList = new List<MagicBall>();

    private bool isInstated;
    private int isCastingStage; 


    public override void Enter(IUnitController controllerInput, GameObject magicBallPrefabInput)
    {
        controller = controllerInput;
        needUpdateState = true;
        animator = controller.GetAnimator;
        characterController = controller.GetCharacterController;

        magicBallPrefab = magicBallPrefabInput;
        animator.SetBool("Spell1", true);
        isInstated = false;
        isCastingStage = 0;

        if (magicBallList == null)
        {
            magicBallList = new List<MagicBall>();
        }
        else
        {
            magicBallList.Clear();
        }

    }

    public override void Update()
    {
        if (!isInstated)
        {
            SpawnFireBalls();
            isInstated = true;
        }

        if ( (isCastingStage == 0) && !controller.NotInAnimating)
        {
            isCastingStage = 1;
        }
        else if (isCastingStage == 1 && controller.NotInAnimating)
        {
            foreach (MagicBall magicBall in magicBallList)
            {
                magicBall.NeedMoving = true;
            }
            isCastingStage = 2;
        }
        else if (isCastingStage == 2 && !controller.NotInAnimating)
        {
            isCastingStage = 3;
        }
        else if (isCastingStage == 3 && controller.NotInAnimating)
        {
            needUpdateState = false;
        }

    }

    public override void Exit()
    {
        needUpdateState = false;
        animator.SetBool("Spell1", false);
    }

    private void SpawnFireBalls()
    {
        // Magic ball 1 (forward)
        Vector3 spawnPos1 = characterController.transform.position +
            characterController.transform.forward * GMData.Instance.MAGIC_BALL_OFFSET
            + Vector3.up * 2f;
        Vector3 direction1 = characterController.transform.forward;
        Quaternion rot1 = Quaternion.LookRotation(direction1);
        magicBallList.Add(MagicBallControll(spawnPos1, direction1, rot1));


        // Magic ball 2 (right)
        Vector3  spawnPos2 = spawnPos1 + characterController.transform.right * GMData.Instance.MAGIC_BALL_OFFSET;
        Vector3 direction2 = Quaternion.AngleAxis(15f, Vector3.up) * direction1;
        Quaternion rot2 = Quaternion.LookRotation(direction2);
        magicBallList.Add(MagicBallControll(spawnPos2, direction2, rot2));

        // Magic ball 3 (left)
        Vector3 spawnPos3 = spawnPos1 - characterController.transform.right * GMData.Instance.MAGIC_BALL_OFFSET;
        Vector3 direction3 = Quaternion.AngleAxis(-15f, Vector3.up) * direction1;
        Quaternion rot3 = Quaternion.LookRotation(direction3);
        magicBallList.Add(MagicBallControll(spawnPos3, direction3, rot3));

    }

    private MagicBall MagicBallControll(Vector3 spawnPos, Vector3 direction, Quaternion rotation)
    {
        GameObject mgBall = GameObject.Instantiate(magicBallPrefab, spawnPos, rotation);
        MagicBall mgBallControl = mgBall.GetComponent<MagicBall>();
        mgBallControl.Init(GMData.Instance.GAME_SPEED, GMData.Instance.GAME_SPEED * 2, 
            spawnPos + direction * GMData.Instance.MAX_SPELL_DISTANCE);
        return mgBallControl;
    }


}
