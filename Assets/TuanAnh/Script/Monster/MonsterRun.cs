using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterRun : MonsterState
{
    private IMonsterController monster;

    public override void Enter(IMonsterController inputMonster)
    {
        monster = inputMonster;
        monster.Animator.SetBool("Run", true);
        monster.Agent.isStopped = false;
    }

    public override void Update()
    {
        monster.Agent.SetDestination(PlayerController.Instance.transform.position);
    }


    public override void Exit()
    {
        monster.Animator.SetBool("Run", false);
        monster.Agent.isStopped = true;
    }


}
