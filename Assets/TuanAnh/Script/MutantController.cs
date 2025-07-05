using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MutantScript : MonoBehaviour
{
    float nextTimeThink = 0;
    const float thinkTime = 5f;
    float stoppingDistance = 2f;
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
        if (Time.time > nextTimeThink)
        {
            nextTimeThink = Time.time + thinkTime;
            agent.SetDestination(MageController.Instance.transform.position);
        }

        
        if (agent.velocity.magnitude > 0.2f)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        
    }


    
}
