using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public static GM Instance { get; private set; }
    
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
