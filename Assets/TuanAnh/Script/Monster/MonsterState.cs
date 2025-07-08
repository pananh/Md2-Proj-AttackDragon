using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Unity.VisualScripting;

public class MonsterState : State
{
    public virtual void Enter(IMonsterController inputMonster)
    {
    }

    public virtual void Update()
    {
    }

    public virtual bool NeedUpdateState()
    {
        return true;
    }

    public virtual void Exit()
    {
    }



}
