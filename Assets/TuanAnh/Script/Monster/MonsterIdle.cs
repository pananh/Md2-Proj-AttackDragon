using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdle : MonsterState
{
    private IMonsterController controller;
    //private bool needUpdateState;
    

    public override void Enter(IMonsterController inputMonster)
    {
        controller = inputMonster;
        controller.Animator.SetBool("Idle", true);
        controller.Agent.isStopped = true;
    }

    public override void Exit()
    {
        controller.Animator.SetBool("Idle", false);
        
    }


}
