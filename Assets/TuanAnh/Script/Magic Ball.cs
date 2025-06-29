using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MagicBall : MonoBehaviour
{

    private float speed;
    private float timeFly;
    private float maxTime;
    private Vector3 targetBall;
    private float ballRadius = 0.5f;
    private Vector3 direction;
    private float castDistance;
    private RaycastHit hit;

    private bool needMoving;
    public bool NeedMoving
    {
        get => needMoving;
        set => needMoving = value;
    }



    public void Init(float speed, float MaxTime, Vector3 targetBall)
    {
        timeFly = 0f;
        this.speed = speed;
        this.maxTime = MaxTime;
        this.targetBall = targetBall;
        needMoving = false;
        direction = (targetBall - transform.position).normalized;
    }


    void Update()
    {
        if (!needMoving)
        {
            return;
        }
        MoveForward();
    }

    private void MoveForward()
    {
        timeFly += Time.deltaTime;
        castDistance = speed * Time.deltaTime;
        if (Physics.SphereCast(transform.position, ballRadius, direction, out RaycastHit hit, castDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                hit.collider.GetComponent<MageController>().TakeDamage(1);
                Destroy(gameObject);
                return;
            }
            else if (hit.collider.CompareTag("Ground"))
            {   
                Debug.Log("Hit the ground");
                Destroy(gameObject);
                return;
            }
        }






        transform.position = Vector3.MoveTowards(transform.position, targetBall, speed * Time.deltaTime);
        if (timeFly > maxTime)
        {
            Destroy(gameObject);
        }



    }

   

}
