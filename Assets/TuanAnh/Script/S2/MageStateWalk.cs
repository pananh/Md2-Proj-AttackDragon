using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class MageStateWalk : MageState
{
    private CharacterController thisCharacterController;
    private Vector3 thisDestination;
    private const float MAX_UPDATE_STATE_TIME = 3f;

    public override float MaxUpdateTime()
    { return MAX_UPDATE_STATE_TIME; }

    LineRenderer lineRenderer;
    Animator animator;

    public override void Enter()
    {

    }

    public override void Enter(Vector3 Destination, CharacterController characterController)
    {
        thisDestination = Destination;
        thisDestination.y = 0;
        thisCharacterController = characterController;



        Vector3 lockDirection = thisDestination - thisCharacterController.transform.position;
        lockDirection.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lockDirection);
        thisCharacterController.transform.rotation = rotation;





        animator = MageController.instance.GetComponent<Animator>();
        animator.SetBool("walking", true);

        
        
        
        
        lineRenderer = MageController.instance.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, thisCharacterController.transform.position);




    }

    public override void Update()
    {
        if ( !IsDestinationReached() )
        {
            MoveCharacter();
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, thisCharacterController.transform.position);




        }

    }


    public override void Exit()
    {

        animator.SetBool("walking", false);


    }

    private void MoveCharacter()
    {
        Vector3 direction = thisDestination - thisCharacterController.transform.position;
        Vector3 lockDirection = direction;
        lockDirection.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lockDirection);
        //thisCharacterController.transform.rotation = Quaternion.Slerp(thisCharacterController.transform.rotation, rotation, GM.instance.SpeedGame * Time.deltaTime);
        //thisCharacterController.transform.rotation = rotation;
        
        thisCharacterController.Move(direction.normalized * GM.instance.SpeedGame * Time.deltaTime);
    }

    private bool IsDestinationReached()
    {
        Vector3 distanceVector = thisCharacterController.transform.position - thisDestination;
        distanceVector.y = 0;
        if (distanceVector.sqrMagnitude < GM.MIN_MOVE_SQR_DISTANCE)
        {
            return true;
        }
        return false;

    }

}
