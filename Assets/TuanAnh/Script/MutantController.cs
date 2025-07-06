using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MutantController : MonoBehaviour
{
    private float nextTimeThink = 0;
    private const float thinkTime = 2f;
    private float stoppingDistance = 1.5f;
    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stoppingDistance;
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if ((Time.time < nextTimeThink) || !IsTargetOnNavMesh(PlayerController.Instance.transform.position))
        {
            return;
        }
        nextTimeThink = Time.time + thinkTime;
        agent.SetDestination(PlayerController.Instance.transform.position);



        if (agent.velocity.magnitude > GMData.Instance.MIN_MOVE_DISTANCE)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        if ( IsTargetInRange (PlayerController.Instance.transform.position) )
        {
            transform.LookAt(PlayerController.Instance.transform.position);
            animator.SetBool("Punch", true);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(" Trigger Entered 222 " + other.name);
        if (other.CompareTag("Player"))
        {
            //PlayerController.Instance.GetDamage(1);
            //animator.SetBool("Punch", false);

            Debug.Log("Player Entered Trigger");

        }
    }



    private bool IsTargetOnNavMesh(Vector3 targetPosition)
    {
        NavMeshHit hit;
        float maxDistance = 0.5f; // bán kính kiểm tra, có thể điều chỉnh
        return NavMesh.SamplePosition(targetPosition, out hit, maxDistance, NavMesh.AllAreas);
    }

    private bool IsTargetInRange(Vector3 targetPosition)
    {
        float distance = Vector3.Distance(transform.position, targetPosition);
        return distance <= agent.stoppingDistance;
    }



}
