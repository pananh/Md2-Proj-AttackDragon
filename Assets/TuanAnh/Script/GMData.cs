using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMData : MonoBehaviour
{
    public static GMData Instance { get; private set; }
    [SerializeField] GameData gSetting;
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

    public float MAGIC_BALL_OFFSET
    {
        get => gSetting.magicBallOffset;
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
