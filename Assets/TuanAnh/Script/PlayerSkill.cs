using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    Animator animatorControl;

    // Start is called before the first frame update



    void Start()
    {
        animatorControl = GetComponent<Animator>();
    }

    void Update()
    {

        //if (Input.GetKey(KeyCode.T))
        //{
        //    animatorControl.SetBool("SkillKick", true);
        //}
        //if ( animatorControl.GetBool("SkillKick") && Input.GetKeyUp(KeyCode.T))
        //{
        //    animatorControl.SetBool("SkillKick", false);
        //}

      


    }










}
