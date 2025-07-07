using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


enum MonsterState
{
    Idle,
    Chase,
    Attack
}

public class MutantController : MonoBehaviour
{
  
    private NavMeshAgent agent;
    private Animator animator;

    [SerializeField] private MonsterData inputMonData;
    private MonsterData monsterData;
    private float sqrMonsterVision;
    private float sqrAttackRange;
    private float sqrDistanceToTarget;
    MonsterState state;
    private float thinkTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        monsterData = inputMonData.CloneData();
        agent.stoppingDistance = monsterData.attackRange;
        agent.speed = monsterData.speed;
        sqrMonsterVision = monsterData.visionRange * monsterData.visionRange;
        sqrAttackRange = monsterData.attackRange * monsterData.attackRange + 0.1f;
        thinkTime = 2f;

    }

    void Update()
    {
        sqrDistanceToTarget = Vector3.SqrMagnitude(PlayerController.Instance.transform.position - transform.position);

        if (sqrDistanceToTarget > GMData.Instance.MAX_MOVE_SQR_DISTANCE)
        {
            MonsterIdle();
            return;
        } 
        else if (sqrDistanceToTarget > GMData.Instance.MIN_MOVE_SQR_DISTANCE)
        {
            MonsterChase();
        }
        else
        {
            MonsterAttack();
        }



       



    }

    private void MonsterIdle()
    {
        if (state != MonsterState.Idle)
        {
            state = MonsterState.Idle;
            agent.isStopped = true;
            animator.SetBool("Run", false);
            animator.SetBool("Punch", false);
        }
    }

    private void MonsterChase()
    {
        //thinkTime -= Time.deltaTime;
        //if (thinkTime <= 0f)
        //{
        //    thinkTime = 2f;
        //    if (sqrDistanceToTarget > sqrMonsterVision)
        //    {
        //        MonsterIdle();
        //        return;
        //    }
        //}

        if (state != MonsterState.Chase)
        {
            state = MonsterState.Chase;
            agent.isStopped = false;
            agent.SetDestination(PlayerController.Instance.transform.position);
            animator.SetBool("Run", true);
            animator.SetBool("Punch", false);
        }
    }
    
    private void MonsterAttack()
    {
        if (state != MonsterState.Attack)
        {
            state = MonsterState.Attack;
            agent.isStopped = true;
            animator.SetBool("Run", false);
            animator.SetBool("Punch", true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //PlayerController.Instance.GetDamage(1);
            animator.SetBool("Punch", false);

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
