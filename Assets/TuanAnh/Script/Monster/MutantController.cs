using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class MutantController : MonoBehaviour, IMonsterController
{
  
    private NavMeshAgent agent;
    public NavMeshAgent Agent => agent;

    private Animator animator;
    public Animator Animator => animator;
    public Transform Transform => this.transform;

    [SerializeField] private MonsterData inputMonData;
    private MonsterData monsterData;
    private float sqrMonsterVision;
    private float sqrAttackRange;
    private float sqrDistanceToTarget;
    
    private float thinkTime;
    private MonsterState currentState;
    
    public event Action <float, IMonsterController> OnHealthChanged;


    public void Init()
    {
        MonsterInit();
        currentState = new MonsterIdle(); 
        currentState.Enter(this);
    }

    void Update()
    {

        sqrDistanceToTarget = Vector3.SqrMagnitude(PlayerController.Instance.transform.position - transform.position);

        if (sqrDistanceToTarget > sqrMonsterVision)
        {
            MonsterIdle();
        } 
        else if (sqrDistanceToTarget > sqrAttackRange)
        {
            MonsterRun();
        }
        else
        {
            MonsterAttack();
        }

    }

    private void MonsterIdle()
    {
       if (currentState is MonsterIdle)
            return;
        currentState.Exit();
        currentState = new MonsterIdle();
        currentState.Enter(this);
        resetThinkTime();
    }

    private void MonsterRun()
    {
        thinkTime -= Time.deltaTime;
        if ( thinkTime > 0 )
            return;

        resetThinkTime();
        if (!(currentState is MonsterRun))
        {
            currentState.Exit();
            currentState = new MonsterRun();
            currentState.Enter(this);
        }
        currentState.Update();
        //agent.SetDestination(PlayerController.Instance.transform.position);

    }
    
    private void MonsterAttack()
    {
        if (currentState is MonsterAttack)
            return;
        currentState.Exit();
        currentState = new MonsterAttack();
        currentState.Enter(this);
        currentState.Update();
    }

    private void MonsterInit()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Initialize monster data
        monsterData = inputMonData.CloneData();
        agent.stoppingDistance = monsterData.attackRange;
        agent.speed = monsterData.speed;
        sqrMonsterVision = monsterData.visionRange * monsterData.visionRange;
        sqrAttackRange = monsterData.attackRange * monsterData.attackRange + 0.1f;

        resetThinkTime();
    }

    private void resetThinkTime()
    {
         thinkTime = monsterData.thinkTime;
    }


    private void TakeDamage(float damage)
    {
        monsterData.maxHealth -= damage;
        OnHealthChanged?.Invoke(monsterData.maxHealth, this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.Instance.TakeDamage(monsterData.attack);
            Debug.Log("Attack Player");
        }
    }





    //private bool IsTargetOnNavMesh(Vector3 targetPosition)
    //{
    //    NavMeshHit hit;
    //    float maxDistance = 0.5f; // bán kính kiểm tra, có thể điều chỉnh
    //    return NavMesh.SamplePosition(targetPosition, out hit, maxDistance, NavMesh.AllAreas);
    //}



}
