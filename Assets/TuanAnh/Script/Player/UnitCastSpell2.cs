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

    private GameObject magicBallPrefab;
    private MagicSphere magicSphere;
    private bool isInstated;
    private int isCastingStage;


    public override void Enter(IUnitController InputController, GameObject magicBallPrefabInput)
    {
        controller = InputController;
        characterController = controller.GetCharacterController;
        animator = controller.GetAnimator;

        magicBallPrefab = magicBallPrefabInput;
        needUpdateState = true;
        animator.SetBool("Spell2", true);
        isInstated = false;
        isCastingStage = 0;

    }

    public override void Update()
    {
        if (!isInstated)
        {
            SpawnSphereBall();
            isInstated = true;
        }
        if ((isCastingStage == 0) && !controller.NotInAnimating) // Bat dau lam phep: Chuan bi 
        {
            isCastingStage = 1;
        }
        else if (isCastingStage == 1 && controller.NotInAnimating) // Ket thuc chuan bi
        {
                magicSphere.IsBigger = true;                        // Magic Ball bat dau di chuyen
                isCastingStage = 2;                         // Chuyen sang giai doan phep 2
        }
        else if (isCastingStage == 2 && !controller.NotInAnimating) // Bat dau thuc hien giai doan 2.
        {
            isCastingStage = 3;
        }
        else if (isCastingStage == 3 && controller.NotInAnimating) // Ket thuc lam phep thuat
        {
            magicSphere.IsExplode = true;
            needUpdateState = false;
        }


    }

    public override void Exit()
    {
        needUpdateState = false;
        animator.SetBool("Spell2", false);
    }

    private void SpawnSphereBall()
    {
        GameObject sphereBall = Object.Instantiate(magicBallPrefab, characterController.transform.position + Vector3.up * 1.5f, Quaternion.identity);
        magicSphere = sphereBall.GetComponent<MagicSphere>();
        magicSphere.Init();
    }

}
