using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IMonsterController
{
    Animator Animator { get; }
    NavMeshAgent Agent { get; }
    Transform Transform { get; }
    
    event Action <float> MonsterOnHealthChanged;
    
    MonsterData MonsterData { get; }

    void Init();
}
