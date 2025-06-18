using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public abstract class MageState
{
    public virtual void Enter (MageController player)
    {

    }

    public virtual void Update()
    {
    }

    public virtual bool NeedUpdateState()
    {
        return true;
    }

    public virtual void Exit()
    {
    }
}
