using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFlag : MonoBehaviour
{
    
    private bool notInAnimating = true;
    public bool NotInAnimating
    {
        get => notInAnimating;
        set => notInAnimating = value;
    }

    public void FlagInOutAnimating()
    {
        notInAnimating = !notInAnimating;
    }

}
