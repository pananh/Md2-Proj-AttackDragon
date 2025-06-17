using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class MageController : MonoBehaviour
{
    public static MageController instance { get; private set; }
    private MageState currentSate;
    private float startStateTime = 0f;

    private CharacterController characterController;

    Vector3 destination;




    public void Awake()
    {
        instance = this;
    }


    public void Init()
    {
        characterController = GetComponent<CharacterController>();
        currentSate = new MageStateIdle();
        
        destination = transform.position;
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            currentSate.Exit();
            currentSate = new MageStateWalk();
            destination = GetDestination();
            currentSate.Enter(destination, characterController);
            startStateTime = Time.time;

        }
        if (Time.time - startStateTime < currentSate.MaxUpdateTime())
        {
            currentSate.Update();
        }
        else
        {
            currentSate.Exit();
            currentSate = new MageStateIdle();
            currentSate.Enter();
        }




    }

    private static Vector3 GetDestination()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, GM.RAYCAST_DISTANCE))
        {
            Vector3 vector3 = hit.point - instance.transform.position;
            if (vector3.sqrMagnitude < GM.MIN_MOVE_SQR_DISTANCE)
            {
                return instance.transform.position;
            }
            else if (vector3.sqrMagnitude > GM.MAX_MOVE_SQR_DISTANCE)
            {
                return instance.transform.position + vector3.normalized * GM.MAX_MOVE_DISTANCE;
            }
            else return hit.point;
        }
        else
        {
            return instance.transform.position;  
        }

    }


}
