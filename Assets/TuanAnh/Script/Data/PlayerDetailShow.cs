using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDetailShow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDetail;
    [SerializeField] private TextMeshProUGUI textDisplayName;
    [SerializeField] private Image playerImage;
    private int id;

    public void SetData(PlayerData playerData)
    {
        id = playerData.id;
    }

    public void OnClickItem()
    {
    }

    public void DisplayPlayerDetail(PlayerData playerData)
    {
        textDisplayName.text = playerData.playerName;
        textDetail.text = "Level: " + playerData.playerLevel + "\n" +
                     "Health: " + playerData.health + "\n" +
                     "Mana: " + playerData.mana + "\n" +
                     "Speed: " + playerData.speed + "\n" +
                     "Attack Power: " + playerData.attackPower + "\n" +
                     "Defense: " + playerData.defense;
        playerImage.sprite = playerData.playerImage;
        Debug.Log("Player Detail Displayed: " + playerData.playerName);
    }
}

