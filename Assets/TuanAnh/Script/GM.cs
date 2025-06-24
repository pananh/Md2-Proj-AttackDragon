using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public static GM Instance { get; private set; }

    [SerializeField] private int gameSpeed = 5;
    public int GAME_SPEED 
    {
        get => gameSpeed; 
        set => gameSpeed = value; 
    }

    public static float RAYCAST_DISTANCE
    {
        get => 100f;
    }
    public static float MIN_MOVE_DISTANCE
    {   get => 0.01f; 
    }
    public static float MIN_MOVE_SQR_DISTANCE
    {   get => 0.01f;
    }
    public static float MIN_STUCK_DISTANCE
    {   get => 0.001f;
    }
    public static float MAX_MOVE_DISTANCE
    {   get => 15f;
    }
    public static float MAX_MOVE_SQR_DISTANCE
    {   get => 200f;
    }
    public static float GRAVITY
    {   get => -9.81f; 
    }


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        MageController.Instance.Init();
        
    }

    void Update()
    {
        
    }
}
