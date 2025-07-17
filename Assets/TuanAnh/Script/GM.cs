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
        EnterGame();
    }

    public void EnterGame()
    {
        PlayerController.Instance.Init();
        MonsterManager.Instance.Init();

        UIMinimap.Instance.Init();
        HealthBarManager.Instance.Init();
        SoundManager.Instance.Init();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
    }

}
