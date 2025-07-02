using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public static Bomb instance;

    [SerializeField] private GameObject trueRemoveButtonPrefab;
    [SerializeField] private GameObject falseRemoveButtonPrefab;
    private bool activeBomb = false;
    public bool ActiveBomb
    {
        get { return activeBomb; }
        set { activeBomb = value; }
    }

    private bool countDown = false;
    private float timer = 10f;
    private int trueButtonNumber = 2;
    private int falseButtonNumber = 3;
    private float spawnRadius = 4f;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!countDown && activeBomb)
        {
           ReduceSize();
           SpawnButtons();
           countDown = true;
           CanvasBomb.instance.ShowGameState(true, "Left click to remove bomb!");
            CanvasBomb.instance.ShowTrueButton(true, trueButtonNumber);

        }
        if (countDown)
        {
            timer -= Time.deltaTime;
            CanvasBomb.instance.ShowCountDown(true, timer);
            if (timer <= 0f)
            {
                EndGame();
            }
            ClickToRemoveBomb();
        }


    }

    private void ClickToRemoveBomb()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 50f))
            {
                if (hit.collider.CompareTag("TrueButton"))
                {
                    Destroy(hit.collider.gameObject);
                    trueButtonNumber--;
                    CanvasBomb.instance.ShowTrueButton(true, trueButtonNumber);
                    Debug.Log("True button removed. Remaining: " + trueButtonNumber);
                }
                else if (hit.collider.CompareTag("FalseButton"))
                {
                    EndGame();
                }
                if (trueButtonNumber == 0 )
                {
                    WinGame();
                }
            }


        }
    }

    private void WinGame()
    {
        CanvasBomb.instance.ShowGameState(true, "Win");
        Time.timeScale = 0f; // Stop the game
    }

    private void EndGame()
    {
        CanvasBomb.instance.ShowGameState(true, "Game Over");
        Time.timeScale = 0f; // Stop the game
    }

    private void ReduceSize()
    {
        transform.localScale *= 0.5f;
    }

    private void SpawnButtons()
    {
        for (int i = 0; i < trueButtonNumber; i++)
        {
            Vector3 pos = GetRandomPositionAroundBomb();
            Instantiate(trueRemoveButtonPrefab, pos, Quaternion.identity);
            Debug.Log("Spawned true button at: " + pos);
        }


        for (int i = 0; i < falseButtonNumber; i++)
        {
            Vector3 pos = GetRandomPositionAroundBomb();
            Instantiate(falseRemoveButtonPrefab, pos, Quaternion.identity);
            Debug.Log("Spawned false button at: " + pos);
        }
    }

    private Vector3 GetRandomPositionAroundBomb()
    {
        
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPos = transform.position + new Vector3(randomCircle.x, 0, randomCircle.y);
        return spawnPos;
    }
}

