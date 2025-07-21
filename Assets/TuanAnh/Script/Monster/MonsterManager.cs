using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager Instance { get; private set; }
    [SerializeField] private float numberOfMonster = 15f;
    public float NumberOfMonster { get => numberOfMonster; set => numberOfMonster = value; }

    [SerializeField] private GameObject monsterPrefab;
    private List<IMonsterController> monsterList; // dung list nay de su dung cac ham trong game quan ly cho de
    public List<IMonsterController> MonsterList
    {
        get => monsterList;
    }


    [SerializeField] private GameObject spawnObjectList;
    private List <Vector3> spawnPointsList;


    private void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        SpawnMonster();
        InitMonster();
    }

    

    private void InitMonster()
    {
        foreach (IMonsterController monster in monsterList)
        {
            monster.Init();
        }
    }

    private void SpawnMonster()
    {
        if (monsterList == null) monsterList = new List<IMonsterController>();
        for (int i = 0; i < numberOfMonster; i++)
        {
            GameObject monsterObject = Instantiate(monsterPrefab, RandomSpawnPoint(), Quaternion.identity);
            IMonsterController monsterController = monsterObject.GetComponent<IMonsterController>();
            monsterList.Add(monsterController);
        }
    }

    public void AddMonster(IMonsterController monster)
    {
        monsterList.Add(monster);
    }

    public void RemoveMonster(IMonsterController monster)
    {
        if (monsterList.Contains(monster))
        {
            monsterList.Remove(monster);
        }
    }



    private Vector3 RandomSpawnPoint()
    {
        if (spawnPointsList == null || spawnPointsList.Count == 0)
        {
            InitSpawnPoint();
        }
        return spawnPointsList[Random.Range(0, spawnPointsList.Count)] +
                new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
    }

    private void InitSpawnPoint()
    {
        spawnPointsList = new List<Vector3>();
        for (int i = 0; i < spawnObjectList.transform.childCount; i++)
        {  spawnPointsList.Add(spawnObjectList.transform.GetChild(i).position); }
    }

    public void ResetInstance()
    {
        monsterList.Clear();
        spawnPointsList.Clear();
        Instance = null;
    }

    //    Dictionary<Collider, IMonsterController> monsterMap = new Dictionary<Collider, IMonsterController>();
    //    // Khi spawn:
    //    monsterMap[monsterCollider] = monsterController;
    //// Khi va chạm:
    //if (monsterMap.TryGetValue(hit.collider, out var controller))
    //{
    //    controller.TakeDamage(1f);
    //}

}
