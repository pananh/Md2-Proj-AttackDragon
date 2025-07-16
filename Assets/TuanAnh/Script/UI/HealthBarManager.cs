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
    private Dictionary<IMonsterController, HealthBar> monsterHbDict;

    private void Awake()
    {
        Instance = this;
       
    }

    public void Init()
    {
        canvas = GetComponent<Canvas>();
        canvasRect = GetComponent<RectTransform>();

        InitPlayerHealthBar();
        InitMonsterHealthBar();
    }


    private void InitPlayerHealthBar()
    {
        GameObject hbObject = Instantiate(hbPrefabPlayer, canvas.transform);
        playerHb = hbObject.GetComponent<HealthBar>();
       
        playerHb.Init(PlayerController.Instance.transform, PlayerController.Instance.CurrentPlayerData.maxHealth, 
            PlayerController.Instance.CurrentPlayerData.maxHealth);
       
        PlayerController.Instance.PlayerOnHealthChanged += PlayerUpdateHb;  
    }

    private void InitMonsterHealthBar()
    {
        if (monsterHbDict == null) monsterHbDict = new Dictionary<IMonsterController, HealthBar>();

        foreach (IMonsterController monster in MonsterManager.Instance.MonsterList)
        {
            GameObject hbObject = Instantiate(hbPrefabMonster, canvas.transform);
            HealthBar monsterHb = hbObject.GetComponent<HealthBar>();
            monsterHb.Init(monster.Transform, monster.MonsterData.maxHealth, monster.MonsterData.currentHealth);
            monsterHbDict.Add(monster, monsterHb);

            // Gan dang su kien cho tung con quai, con nao di voi mau con do, truyen currentHealth vao Hb Bar
            monster.MonsterOnHealthChanged += (currentHealth) => 
            {
                monsterHb.CurrentHealth = currentHealth;
            };

        }
    }

    public void RemoveMonsterHealthBar(IMonsterController monster)
    {
        Destroy(monsterHbDict[monster].gameObject);
        monsterHbDict.Remove(monster);
        
    }

    private void PlayerUpdateHb(float currentHealth)
    {
        playerHb.CurrentHealth = currentHealth;
    }

   
}
