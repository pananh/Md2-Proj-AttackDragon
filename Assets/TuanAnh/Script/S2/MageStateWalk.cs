using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class MageStateWalk : MageState
{
    private CharacterController thisCharacterController;
    private Vector3 thisDestination;
    private bool needToUpdate = false;
    private float verticalVelocity = 0f;

    public override bool NeedToUpdate()
    { return needToUpdate;
    }


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

        animator = MageController.instance.GetComponent<Animator>();
        animator.SetBool("walking", true);

        needToUpdate = true;



        lineRenderer = MageController.instance.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, thisCharacterController.transform.position);


        RotateCharacter();

    }

    public override void Update()
    {
        if ( needToUpdate )
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

    private void RotateCharacter()
    {
        Vector3 direction = thisDestination - thisCharacterController.transform.position;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        thisCharacterController.transform.rotation = targetRotation;
    }

    private void MoveCharacter()
    {
        Vector3 direction = thisDestination - thisCharacterController.transform.position;
        direction.y = 0;

        // Kiem tra neu da den dich
        if (direction.sqrMagnitude < GM.MIN_MOVE_SQR_DISTANCE)
        {
            Debug.Log("Reached destination: " + thisDestination);
            needToUpdate = false;
            return;
        }

        // Xu ly gravity
        if (thisCharacterController.isGrounded)
        {
            verticalVelocity = -1f; // Dam bao dinh mat dat
        }
        else
        {
            verticalVelocity += GM.GRAVITY * Time.deltaTime;
        }

        Vector3 oldPosition = thisCharacterController.transform.position;
        thisCharacterController.Move(direction.normalized * GM.instance.SpeedGame* 0.55f *Time.deltaTime);

        // Neu bi ket o doc cao thi dung lai
        if ((thisCharacterController.transform.position - oldPosition).sqrMagnitude < GM.MIN_STUCK_DISTANCE)
        {
            needToUpdate = false;
            Debug.Log("Stuck at: " + thisCharacterController.transform.position);
        }


    }



}
