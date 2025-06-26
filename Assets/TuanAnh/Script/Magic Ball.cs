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
        transform.position = Vector3.MoveTowards(transform.position, targetBall, speed * Time.deltaTime);
        if (timeFly > maxTime)
        {
            Destroy(gameObject);
        }
    }

   

}
