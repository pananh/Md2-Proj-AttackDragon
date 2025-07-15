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
    private List<Image> monsterIconList;


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
        monsterIconList = new List<Image>();
        foreach (IMonsterController monster in MonsterManager.Instance.MonsterList)
        {
            Image singleMonsterIcon = Instantiate(mosterIconPrefab, miniMap);
            singleMonsterIcon.enabled = true;
            monsterIconList.Add(singleMonsterIcon);
        }

    }

    private void UpdateMonsterLocation()
    {
        for (int i = 0; i < monsterIconList.Count; i++)
        {
            SetIconLocation(MonsterManager.Instance.MonsterList[i].Transform.position,
                monsterIconList[i]);
        }
    }

    private void UpdatePlayerLocation()
    {
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

}

