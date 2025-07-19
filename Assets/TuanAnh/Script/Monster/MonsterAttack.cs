using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterAttack : MonsterState
{
    private IMonsterController monster;

    public override void Enter(IMonsterController inputMonster)
    {
        monster = inputMonster;
        monster.Animator.SetBool("Attack", true);
        monster.Agent.isStopped = true;
         
    }

    public override void Update()
    {
        return; // Do nothing, monster is attacking
 
        //LookAtPlayer();

    }


    public override void Exit()
    {
        monster.Animator.SetBool("Attack", false);
        monster.Agent.isStopped = false;
    }


    // Su dung Root Transform Position, va khoa Bake Into Pose nen khong can ham nay nua
    //private void LookAtPlayer()
    //{
    //    Vector3 directionToPlayer = monster.Transform.position - PlayerController.Instance.transform.position;
    //    float angle = Vector3.Angle(monster.Transform.forward, directionToPlayer);
    //    if (angle < 2f) return;
    //    Debug.Log("Monster is looking at player");
    //    monster.Transform.LookAt(PlayerController.Instance.transform.position);
    //}
}
