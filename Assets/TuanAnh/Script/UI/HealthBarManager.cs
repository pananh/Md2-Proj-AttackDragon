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
    private PlayerData playerData;


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
        playerData = PlayerController.Instance.CurrentPlayerData;

        playerHb.SetPosition(PlayerController.Instance.transform);
        UpdatePlayerDataToHealthBar();
        PlayerController.Instance.EvPlayerDataChanged += PlayerUpdateData;  
    }

    private void InitMonsterHealthBar()
    {
        if (monsterHbDict == null) monsterHbDict = new Dictionary<IMonsterController, HealthBar>();

        foreach (IMonsterController monster in MonsterManager.Instance.MonsterList)
        {
            GameObject hbObject = Instantiate(hbPrefabMonster, canvas.transform);
            HealthBar monsterHb = hbObject.GetComponent<HealthBar>();
            monsterHbDict.Add(monster, monsterHb);
            monsterHb.SetPosition(monster.Transform);
            UpdateMonsterDataToHealthBar(monster);

            // Gan dang su kien cho tung con quai, con nao di voi mau con do, truyen currentHealth vao Hb Bar
            monster.MonsterOnHealthChanged += (currentHealth) => 
            {
                monsterHb.SetHealthData(currentHealth, monster.MonsterData.maxHealth);
            };

        }
    }

    private void UpdateMonsterDataToHealthBar(IMonsterController monster)
    {
         monsterHbDict[monster].SetHealthData(monster.MonsterData.currentHealth, monster.MonsterData.maxHealth);
         monsterHbDict[monster].SetLevel(monster.MonsterData.level);
         monsterHbDict[monster].SetAboveText(monster.MonsterData.monsterName);
    }

    public void RemoveMonsterHealthBar(IMonsterController monster)
    {
        Destroy(monsterHbDict[monster].gameObject);
        monsterHbDict.Remove(monster);
        
    }

    private void PlayerUpdateData(PlayerData currentPlayerData)
    {
        playerData = currentPlayerData;
        UpdatePlayerDataToHealthBar();
    }

    private void UpdatePlayerDataToHealthBar()
    {
        playerHb.SetHealthData(playerData.currentHealth, playerData.maxHealth);
        playerHb.SetLevel(playerData.level);
        playerHb.SetAboveText(playerData.exp + "/" + playerData.expNextLevel);
    }

    public void ResetInstance()
    {
        monsterHbDict.Clear();
        Instance = null;
    }

}
