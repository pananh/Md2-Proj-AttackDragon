using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System;

public abstract class UnitState: State
{
    public virtual void Enter (IUnitController player)
    {

    }

    public virtual void Enter (IUnitController player, Vector3 destination)
    {

    }

    public virtual void Enter (IUnitController player, float variation)
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
