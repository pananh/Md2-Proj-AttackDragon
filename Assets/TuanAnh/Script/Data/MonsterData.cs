using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[CreateAssetMenu(fileName = "MawMonsterData", menuName = "Game/MawMonsterData", order = 3)]
public class MonsterData : ScriptableObject
{
    public int id = 1;
    public string monsterName = "Maw";
    public float level = 1;
    public float maxHealth = 100;
    public float currentHealth = 100;
    public float attack = 5;
    public float speed = 5;
    public float gainExp = 40;
    public Sprite image;
    public float levelUpFactor = 1.05f;
    
    public float attackRange = 1.5f;
    public float visionRange = 45f;
    public float thinkTime = 1.5f;

    public MonsterData CloneData()
    {
        MonsterData clone = ScriptableObject.CreateInstance<MonsterData>();
        clone.id = this.id;
        clone.monsterName = this.monsterName;
        clone.level = this.level;
        clone.maxHealth = this.maxHealth;
        clone.currentHealth = this.currentHealth;
        clone.attack = this.attack;
        clone.speed = this.speed;
        clone.gainExp = this.gainExp;
        clone.image = this.image;
        clone.levelUpFactor = this.levelUpFactor;

        clone.attackRange = this.attackRange;
        clone.visionRange = this.visionRange;
        clone.thinkTime = this.thinkTime;
        return clone;
    }

    public MonsterData CloneData(float level)
    {
        MonsterData clone = CloneData();
        for (int i = 1; i < level; i++)
        {
            clone.LevelUp(clone);
        }
        return clone;
    }

    public void LevelUp(MonsterData currentMonster)
    {
        currentMonster.level++;
        currentMonster.maxHealth *= levelUpFactor;
        currentMonster.currentHealth = currentMonster.maxHealth;
        currentMonster.attack *= levelUpFactor;
        currentMonster.speed *= levelUpFactor;
        if (currentMonster.speed > 10f)
        {
            currentMonster.speed = 10f;
        }
        currentMonster.gainExp *= levelUpFactor;

    }

}
