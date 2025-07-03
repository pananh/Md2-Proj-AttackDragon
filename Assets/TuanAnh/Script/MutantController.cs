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
        Debug.Log(animator.name + " is the animator of " + gameObject.name);
        if (animator == null)
        Debug.LogError("Animator component is missing!");
       

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextTimeThink)
        {
            nextTimeThink = Time.time + thinkTime;
            agent.SetDestination(MageController.Instance.transform.position);
        }

        
        if (agent.velocity.magnitude > 0.1f)
        {
            Debug.Log("Mutant is running");
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        // Nếu Mutant đã đến gần MageController, dừng lại
        //if (Vector3.Distance(transform.position, MageController.Instance.transform.position) <= stoppingDistance)
        //{
        //    agent.isStopped = true;
        //}
        //else
        //{
        //    agent.isStopped = false;
        //}
    }


    
}
