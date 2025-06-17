using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public abstract class MageState
{
    public virtual void Enter()
    {
    }

    public virtual void Enter(Vector3 Destination, CharacterController characterController)
    {

    }

    public virtual void Update()
    {
    }

    public virtual float MaxUpdateTime()
    {
        return 10f;
    }

    public virtual void Exit()
    {
    }
}
