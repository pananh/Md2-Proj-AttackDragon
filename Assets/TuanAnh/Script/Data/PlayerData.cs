using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/PlayerData", order = 2)]
public class PlayerData : ScriptableObject
{
    public int id = 1;
    public string playerName = "Mage";
    public int level = 1;
    public float health = 100;
    public float attack = 5;
    public float speed = 5;
    public float exp = 0;
    private float expNextLevel = 100;
    public Sprite image;
    public float levelUpFactor = 1.05f;

    public PlayerData CloneData()
    {
        PlayerData clone = ScriptableObject.CreateInstance<PlayerData>();
        clone.id = this.id;
        clone.playerName = this.playerName;
        clone.level = this.level;
        clone.health = this.health;
        clone.attack = this.attack;
        clone.speed = this.speed;
        clone.exp = this.exp;
        clone.expNextLevel = this.expNextLevel;
        clone.image = this.image;
        clone.levelUpFactor = this.levelUpFactor;
        return clone;
    }

    public PlayerData CloneData(float level)
    {
        PlayerData clone = CloneData();
        for (int i = 1; i < level; i++)
        {
            clone.LevelUp(clone);
        }
        return clone;
    }

    public void LevelUp(PlayerData currentPlayer)
    {
        currentPlayer.level++;
        currentPlayer.health *= levelUpFactor;
        currentPlayer.attack *= levelUpFactor;
        
        if ( (currentPlayer.level % 5) == 0f)
        {             currentPlayer.speed *= levelUpFactor;
        }
        if (currentPlayer.speed > 6.5f)
        {
            currentPlayer.speed = 6.5f; 
        }
        currentPlayer.expNextLevel = currentPlayer.expNextLevel + (currentPlayer.level / 2) * this.expNextLevel; // Example formula for max experience
    }


}

