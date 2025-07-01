using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerScrollView : MonoBehaviour
{
    public UIPlayerScrollView instance;

    private List <PlayerDetailShow> playerList = new List<PlayerDetailShow>();
    [SerializeField] private List <PlayerData> playerDataList = new List<PlayerData>();
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Transform contentScrollView;
    [SerializeField] private GameObject UIITempPrefab;


    public void SpawnItems()
    {
        for (int i = 0; i < playerDataList.Count; i++)
        {
            PlayerData playerData = playerDataList[i];
            GameObject playerItem = Instantiate(UIITempPrefab, contentScrollView);
            PlayerDetailShow playerDetailShow = playerItem.GetComponent<PlayerDetailShow>();



        }

    }

}
