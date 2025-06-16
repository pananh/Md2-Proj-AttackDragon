using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public static GM instance { get; private set; }

    private int speedGame = 20;
    public int SpeedGame
    {
        get { return speedGame; }
        set { speedGame = value; }
    }



    void Start()
    {
        instance = this;

    }

    void Update()
    {
        
    }
}
