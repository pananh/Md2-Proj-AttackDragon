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
            destination = GetDestination();
            if (destination == transform.position)
            {
                return;
            }
            currentSate.Exit();
            currentSate = new MageStateWalk();


            //LineRenderer lineRenderer = GetComponent<LineRenderer>();
            //lineRenderer.positionCount = 2;
            //lineRenderer.SetPosition(0, transform.position);
            //lineRenderer.SetPosition(1, destination);

            currentSate.Enter(destination, characterController);
            Debug.Log("Dang chuyen sang trang thai di chuyen toi " + destination);

        }
        if (currentSate.NeedToUpdate())
        {
            currentSate.Update();
        }
        else if (currentSate is MageStateWalk)
        {
            currentSate.Exit();
            currentSate = new MageStateIdle();
            Debug.Log("Da den dich: " + destination);
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
