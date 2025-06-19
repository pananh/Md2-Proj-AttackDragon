using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class UnitIdleJump : UnitState
{
    private bool needUpdateState = false;
    private MageController mageController;
    private CharacterController characterController;
    private Animator animator;
    float verticalVelocity;

    public override bool NeedUpdateState() => needUpdateState;

    public override void Enter(MageController controller )
    {
        mageController = controller;
        characterController = mageController.GetComponent<CharacterController>();
        animator = mageController.GetComponent<Animator>();
        needUpdateState = true;
        verticalVelocity = GM.Instance.GAME_SPEED/2;
        animator.SetBool("Jump", true);
        // Tu dong huyen sang trang thai InAir tren animator
    }

    public override void Update()
    {
        JumpCharacter();

    }

    public override void Exit()
    {
        animator.SetBool("Jump", false);
        animator.SetBool("InAir", false);
    }


    private void JumpCharacter()
    {
        verticalVelocity += GM.GRAVITY * Time.deltaTime;

        Vector3 jumpMove = mageController.transform.forward * (mageController.JumpForwardSpeed * Time.deltaTime) +
                        Vector3.up * (verticalVelocity * Time.deltaTime);

        characterController.Move(jumpMove);
        if (characterController.isGrounded)
        {
            animator.SetBool("InAir", false);
            // Khi nhay xong thi nhay sang trang thai Idle, Dat InAir = false
            needUpdateState = false;
        }

    }










}
