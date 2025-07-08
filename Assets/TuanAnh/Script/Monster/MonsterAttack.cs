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

    }

    public override void Update()
    {
        LookAtPlayer();
      
    }


    public override void Exit()
    {
        monster.Animator.SetBool("Attack", false);
    }

    private void LookAtPlayer()
    {
        Vector3 directionToPlayer = PlayerController.Instance.transform.position - monster.Transform.position;
        float angle = Vector3.Angle(monster.Transform.forward, directionToPlayer);
        if (angle < 10f) return;
        monster.Transform.LookAt(PlayerController.Instance.transform.position);
    }
}
