using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/PlayerData", order = 2)]
public class PlayerData : ScriptableObject
{
    public int id;
    public string playerName;
    public int playerLevel;
    public float health;
    public float mana;
    public float speed;
    public float attackPower;
    public float defense;
    public Sprite playerImage;

}
