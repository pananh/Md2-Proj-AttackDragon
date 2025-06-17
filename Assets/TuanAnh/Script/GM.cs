using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public static GM instance { get; private set; }

    [SerializeField] private int gameSpeed = 5;
    public int SpeedGame
    {
        get { return gameSpeed; }
        set { gameSpeed = value; }
    }

    public static float RAYCAST_DISTANCE
    {
        get { return 100f; }
    }
    public static float MIN_MOVE_DISTANCE
    {   get { return 0.01f; }
    }
    public static float MIN_MOVE_SQR_DISTANCE
    {   get { return 0.01f; }
    }
    public static float MIN_STUCK_DISTANCE
    {   get { return 0.001f; }
    }
    public static float MAX_MOVE_DISTANCE
    {   get { return 15f; }
    }
    public static float MAX_MOVE_SQR_DISTANCE
    {   get { return 200f; }
    }
    public static float GRAVITY
    {   get { return -9.81f; }
    }


    void Awake()
    {
        instance = this;
    }



    void Start()
    {
        MageController.instance.Init();
    }

    void Update()
    {
        
    }
}
