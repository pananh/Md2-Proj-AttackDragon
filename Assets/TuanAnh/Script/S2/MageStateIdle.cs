using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class MageStateIdle : MageState
{
    private bool needToUpdate = false;
    public override bool NeedToUpdate()
    {
        return needToUpdate ;
    }
    public override void Enter()
    {
        Debug.Log("Mage is idle");
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {

    }



}
