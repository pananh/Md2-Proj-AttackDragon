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
        LookAtPlayer();
      
    }


    public override void Exit()
    {
        monster.Animator.SetBool("Attack", false);
        monster.Agent.isStopped = false;
    }

    private void LookAtPlayer()
    {
        Vector3 directionToPlayer = monster.Transform.position - PlayerController.Instance.transform.position;
        float angle = Vector3.Angle(monster.Transform.forward, directionToPlayer);
        if (angle < 5f) return;
        Debug.Log("Monster is looking at player");
        monster.Transform.LookAt(PlayerController.Instance.transform.position);
    }
}
