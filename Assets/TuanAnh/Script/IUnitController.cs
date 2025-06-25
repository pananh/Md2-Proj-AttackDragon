using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IUnitController
{
    Animator GetAnimator {  get; }

    // Co the dinh nghia kieu ham, neu can xu ly logic
    // Animator GetAnimator()

    CharacterController GetCharacterController { get; }

    bool FlagNotInAnimation { get; }

}

