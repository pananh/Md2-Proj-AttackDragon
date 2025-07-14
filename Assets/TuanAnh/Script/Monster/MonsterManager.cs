using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager Instance { get; private set; }
    private float numberOfMonster = 12f;
    public float NumberOfMonster { get => numberOfMonster; set => numberOfMonster = value; }

    [SerializeField] private GameObject monsterPrefab;
    private List<IMonsterController> monsterList; // dung list nay de su dung cac ham trong game quan ly cho de



    [SerializeField] private GameObject spawnObjectList;
    private List <Vector3> spawnPointsList;



    public void Init()
    {
        SpawnMonster();
    }

    void Update()
    {
        
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


}
