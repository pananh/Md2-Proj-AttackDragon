using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System.Net.Http.Headers;
using sc.terrain.proceduralpainter;

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
            SpawnFireBalls(9);
            isInstated = true;
        }

        if ( (isCastingStage == 0) && !controller.NotInAnimating) // Bat dau lam phep: Chuan bi 
        {
            isCastingStage = 1;
        }
        else if (isCastingStage == 1 && controller.NotInAnimating) // Ket thuc chuan bi
        {
            foreach (MagicBall magicBall in magicBallList)
            {
                magicBall.NeedMoving = true;                        // Magic Ball bat dau di chuyen
            }
            isCastingStage = 2;                         // Chuyen sang giai doan phep 2
        }
        else if (isCastingStage == 2 && !controller.NotInAnimating) // Bat dau thuc hien giai doan 2.
        {
            isCastingStage = 3;
        }
        else if (isCastingStage == 3 && controller.NotInAnimating) // Ket thuc lam phep thuat
        {
            needUpdateState = false;
        }

    }

    public override void Exit()
    {
        needUpdateState = false;
        animator.SetBool("Spell1", false);
    }

    private void SpawnFireBalls(int number)
    {
        if (number <= 0) return;
       
        List<(Vector3 pos, Vector3 direction, Quaternion rot)> spawnPoints = GetMagicBallSpawnPoints(number);
        foreach (var spawnPoint in spawnPoints)
        {
            MagicBall magicBall = MagicBallControll(spawnPoint.pos, spawnPoint.direction, spawnPoint.rot);
            magicBallList.Add(magicBall);
        }
    }

   
    private List<(Vector3 pos, Vector3 direction, Quaternion rot)> GetMagicBallSpawnPoints(int number)
    {
        number = Mathf.Clamp(number, 1, 40);
        List<float> angles = GetAngles(number, 5f);
        var result = new List<(Vector3, Vector3, Quaternion)>();
        Vector3 basePos = characterController.transform.position + Vector3.up * 1.5f;
        Vector3 baseDiction = characterController.transform.forward;
        for (int i = 0; i < angles.Count; i++)
        {
            float angle = angles[i];
            Vector3 direction = Quaternion.AngleAxis(angle, Vector3.up) * baseDiction;
            Vector3 pos = basePos + direction * GMData.Instance.MAGIC_BALL_OFFSET;
            result.Add((pos, direction, Quaternion.LookRotation(direction)));
        }
        return result;
    }

    private List <float> GetAngles(int number, float step)
    {
        // (0, 5f, -5f, 10f, -10f, 15f, -15f, 20f, -20f, 25f, -25f, 30f, -30f)
        // (2.5f, -2.5f, 5f, -5f, 7.5f, -7.5f, 10f, -10f, 12.5f, -12.5f, 15f, -15f)
        List <float> angles = new List<float>();
        number = Mathf.Clamp(number, 1, 40);
        if (number % 2 == 1)
        {   angles.Add(0f);
            for (int i = 1; i <= number / 2; i++)
            {
                angles.Add(i * step);
                angles.Add(-i * step);
            }
        }
        else
        {
            for (int i = 1; i <= number / 2; i++)
            {
                angles.Add(i * step - step/2);
                angles.Add(-i * step + step/2);
            }
        }
        return angles;
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
