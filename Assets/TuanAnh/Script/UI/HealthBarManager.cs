using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    public static HealthBarManager Instance { get; private set;  }

    private Canvas canvas;
    public Canvas Canvas => canvas;

    private RectTransform canvasRect;
    public RectTransform CanvasRect => canvasRect;

    [SerializeField] private GameObject hbPrefabPlayer;
    [SerializeField] private GameObject hbPrefabMonster;
    private HealthBar playerHb;
    private List<HealthBar> monsterHbList;




    private void Awake()
    {
        Instance = this;
       
    }

    public void Init()
    {
        canvas = GetComponent<Canvas>();
        canvasRect = GetComponent<RectTransform>();

        InitPlayerHealthBar();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitPlayerHealthBar()
    {
        GameObject hbObject = Instantiate(hbPrefabPlayer, canvas.transform);
        playerHb = hbObject.GetComponent<HealthBar>();
        playerHb.Init(PlayerController.Instance.gameObject, PlayerController.Instance.CurrentPlayerData.maxHealth, PlayerController.Instance.CurrentPlayerData.maxHealth);
       
        PlayerController.Instance.PlayerOnHealthChanged += PlayerUpdateHb;
    }


    private void PlayerUpdateHb(float currentHealth)
    {
        playerHb.CurrentHealth = currentHealth;
    }

   
}
