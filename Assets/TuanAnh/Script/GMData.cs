using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMData : MonoBehaviour
{
    public static GMData Instance { get; private set; }
    [SerializeField] GameSetting gSetting;
    public int GAME_SPEED 
    {
        get => gSetting.gameSpeed; 
    }

    public float RAYCAST_DISTANCE
    {
        get => gSetting.raycastDistance;
    }
    public float MIN_MOVE_DISTANCE
    {   get => gSetting.minMoveDistance; 
    }
    public float MIN_MOVE_SQR_DISTANCE
    {   get => gSetting.minMoveDistance;
    }
    public float MIN_STUCK_DISTANCE
    {   get => gSetting.minStuckDistance;
    }
    public float MAX_MOVE_DISTANCE
    {   get => gSetting.maxMoveDistance;
    }
    public float MAX_MOVE_SQR_DISTANCE
    {   get => gSetting.maxMoveDistance;
    }
    public float GRAVITY
    {   get => gSetting.gravity; 
    }

    public Vector3 MAGIC_BALL_LOCAL_OFFSET
    {
        get => gSetting.magicBallLocalOffset;
    }

    public float TIME_DURATION_FOR_SPELL_TARGET
    {
        get => gSetting.timeDurationForSpellTarget;
    }


    void Awake()
    {
        Instance = this;
    }

    
}
