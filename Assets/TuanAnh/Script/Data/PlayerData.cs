using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public string playerName;
    public int playerLevel;
    public float health;
    public float mana;
    public float speed;
    public float attackPower;
    public float defense;

}
