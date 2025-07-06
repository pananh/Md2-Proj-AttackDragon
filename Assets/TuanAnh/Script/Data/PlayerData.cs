using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/PlayerData", order = 2)]
public class PlayerData : ScriptableObject
{
    public int id;
    public string playerName;
    public int level;
    public float health;
    public float mana;
    public float speed;
    public float attack;
    public float defense;
    public Sprite image;

    public PlayerData CloneData()
    {
        PlayerData clone = ScriptableObject.CreateInstance<PlayerData>();
        clone.id = this.id;
        clone.playerName = this.playerName;
        clone.level = this.level;
        clone.health = this.health;
        clone.mana = this.mana;
        clone.speed = this.speed;
        clone.attack = this.attack;
        clone.defense = this.defense;
        clone.image = this.image;
        return clone;
    }

}

