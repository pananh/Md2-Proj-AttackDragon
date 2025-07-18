using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class UIMinimap : MonoBehaviour
{
    public static UIMinimap Instance { get; private set; }
    [SerializeField] Image playerIconPrefab;
    private Image playerIcon;


    [SerializeField] Image mosterIconPrefab;
    private Dictionary <IMonsterController, Image> monsterIconDic;


    [SerializeField] RectTransform miniMap;
    [SerializeField] Vector2 worldMin;  // 0.0
    [SerializeField] Vector2 worldMax;  // 200.200
    
    private void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        InitPlayerIcon();
        InitMonstersIcon();
    }

    void LateUpdate()
    {
        UpdatePlayerLocation();
        UpdateMonsterLocation();
    }


    private void InitPlayerIcon()
    {
        playerIcon = Instantiate(playerIconPrefab, miniMap);

    }

    private void InitMonstersIcon()
    {
        monsterIconDic = new Dictionary<IMonsterController, Image>();
        foreach (IMonsterController monster in MonsterManager.Instance.MonsterList)
        {
            Image singleMonsterIcon = Instantiate(mosterIconPrefab, miniMap);
            singleMonsterIcon.enabled = true;
            monsterIconDic.Add(monster, singleMonsterIcon);
        }

    }

    public void RemoveMonsterIcon(IMonsterController monster)
    {
        Destroy(monsterIconDic[monster].gameObject);
        monsterIconDic.Remove(monster);
    }

    private void UpdateMonsterLocation()
    {
        foreach ( var monsterIcon in monsterIconDic)
        {
            SetIconLocation(monsterIcon.Key.Transform.position, monsterIcon.Value);
        }

    }

    private void UpdatePlayerLocation()
    {
        if ( PlayerController.Instance == null || playerIcon == null)
            return;
        SetIconLocation(PlayerController.Instance.transform.position, playerIcon);
    }

    private void SetIconLocation(Vector3 worldPosition, Image iconImage)
    {
        float xNorm = Mathf.InverseLerp(worldMin.x, worldMax.x, worldPosition.x);
        float yNorm = Mathf.InverseLerp(worldMin.y, worldMax.y, worldPosition.z);
        float xPos = Mathf.Lerp(0, miniMap.rect.width, xNorm);
        float yPos = Mathf.Lerp(0, miniMap.rect.height, yNorm);
        iconImage.rectTransform.anchoredPosition = new Vector2(xPos, yPos);
    }

    public void ResetInstance()
    {
        Instance = null;
    }
}

