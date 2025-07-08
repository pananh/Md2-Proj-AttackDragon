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
    }

    public override void Update()
    {
        Debug.Log ("Player position: " + PlayerController.Instance.transform.position);
        monster.Agent.SetDestination(PlayerController.Instance.transform.position);
    }


    public override void Exit()
    {
        monster.Animator.SetBool("Run", false);
        monster.Agent.isStopped = true;
    }


}
