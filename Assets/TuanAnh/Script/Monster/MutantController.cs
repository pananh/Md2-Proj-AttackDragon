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
    


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        monsterData = inputMonData.CloneData();
        agent.stoppingDistance = monsterData.attackRange;
        agent.speed = monsterData.speed;
        sqrMonsterVision = monsterData.visionRange * monsterData.visionRange;
        sqrAttackRange = monsterData.attackRange * monsterData.attackRange + 0.1f;
       
        thinkTime = resetThinkThime();
        currentState = new MonsterIdle(); 
        currentState.Enter(this);
    }

    void Update()
    {
        sqrDistanceToTarget = Vector3.SqrMagnitude(PlayerController.Instance.transform.position - transform.position);

        if (sqrDistanceToTarget > sqrMonsterVision)
        {
            Debug.Log(" Monster is too far away, returning to idle state.");
            MonsterIdle();
        } 
        else if (sqrDistanceToTarget > sqrAttackRange)
        {
            Debug.Log(" Monster is within chase range, chasing the player.");
            MonsterRun();
        }
        else
        {
            Debug.Log(" Monster is close enough to attack the player.");
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
        thinkTime = resetThinkThime();
    }

    private void MonsterRun()
    {
        thinkTime -= Time.deltaTime;
        if ( thinkTime > 0 )
            return; 
        thinkTime = resetThinkThime();

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

    private static float resetThinkThime()
    {
        return 2f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //PlayerController.Instance.GetDamage(1);
            animator.SetBool("Attack", false);

            Debug.Log("Player Entered Trigger");

        }
    }


    //private bool IsTargetOnNavMesh(Vector3 targetPosition)
    //{
    //    NavMeshHit hit;
    //    float maxDistance = 0.5f; // bán kính kiểm tra, có thể điều chỉnh
    //    return NavMesh.SamplePosition(targetPosition, out hit, maxDistance, NavMesh.AllAreas);
    //}



}
