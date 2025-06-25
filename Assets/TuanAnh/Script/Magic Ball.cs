using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MagicBall : MonoBehaviour
{

    private float speed;
    private float timeFly;
    private float maxTime;
    private Vector3 targetPosition;

    private bool needMoving;
    public bool NeedMoving
    {
        get => needMoving;
        set => needMoving = value;
    }



    public void Init(float speed, float MaxTime, Vector3 targetPosition)
    {
        timeFly = 0f;
        this.speed = speed;
        this.maxTime = MaxTime;
        this.targetPosition = targetPosition;
        needMoving = false;
    }


    void Update()
    {
        if (!needMoving)
        {
            return;
        }
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        timeFly += Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if ((Vector3.SqrMagnitude(transform.position - targetPosition) < GM.MIN_MOVE_SQR_DISTANCE) || (timeFly > maxTime))
        {
            Destroy(gameObject);
        }
    }


   
}
