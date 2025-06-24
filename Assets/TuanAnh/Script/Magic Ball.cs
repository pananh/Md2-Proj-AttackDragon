using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MagicBall : MonoBehaviour
{

    private float speed;
    [SerializeField] Vector3 targetPosition = new Vector3 (10,200,300);
    private float timeFly;
    private float maxTime;

    public void Start()
    {
        speed = GM.Instance.GAME_SPEED/2;
        timeFly = 0f;
        maxTime = 5f;
    }


    void Update()
    {
        timeFly += Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if ( (Vector3.SqrMagnitude(transform.position - targetPosition) < GM.MIN_MOVE_SQR_DISTANCE) || (timeFly > maxTime) )
        {
            Destroy(gameObject);
        }
       

    }
}
