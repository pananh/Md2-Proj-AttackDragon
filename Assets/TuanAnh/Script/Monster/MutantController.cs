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
    public MonsterData MonsterData => monsterData;

    private float sqrMonsterVision;
    private float sqrAttackRange;
    private float sqrDistanceToTarget;
    
    private float thinkTime;
    private MonsterState currentState;
    private bool isDead;

    public event Action <float> MonsterOnHealthChanged;


    public void Init()
    {
        MonsterInit();
        currentState = new MonsterIdle(); 
        currentState.Enter(this);
    }

    void Update()
    {
        if (isDead) 
            return;

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
        isDead = false;
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


    public void TakeDamage(float damage)
    {
        monsterData.currentHealth -= damage;
        MonsterOnHealthChanged?.Invoke(monsterData.currentHealth); // truyen gia tri curentHealth ra ngoai cho ai su dung

        if (monsterData.currentHealth <= 0)
        {
            MonsterDie();
        }

    }

    private void MonsterDie()
    {
        if (isDead)
            return;
        isDead = true;
        currentState.Exit();
        currentState = new MonsterDie();
        currentState.Enter(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.Instance.TakeDamage(monsterData.attack);
            Debug.Log("Attack Player");
        }
    }

    public void StartMonsterCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }



    //private bool IsTargetOnNavMesh(Vector3 targetPosition)
    //{
    //    NavMeshHit hit;
    //    float maxDistance = 0.5f; // bán kính kiểm tra, có thể điều chỉnh
    //    return NavMesh.SamplePosition(targetPosition, out hit, maxDistance, NavMesh.AllAreas);
    //}



}
