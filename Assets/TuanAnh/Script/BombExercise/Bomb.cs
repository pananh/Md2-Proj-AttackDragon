using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    Prepare,
    Running,
    GameOver
}
public class Bomb : MonoBehaviour
{
    public static Bomb instance;
    public GameState gameState;
    [SerializeField] private GameObject trueRemoveButtonPrefab;
    [SerializeField] private GameObject falseRemoveButtonPrefab;


    public bool countDown = false;
    private float timer = 10f;
    private int trueButtonNumber = 2;
    private int falseButtonNumber = 3;
    private float spawnRadius = 4f;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        SetGameState(GameState.Running);
    }

    public void Activate()
    {
      
        ReduceSize();
        SpawnButtons();
        countDown = true;
 
        CanvasBomb.instance.ShowGameState(true, "Left click to remove bomb!");
        CanvasBomb.instance.ShowTrueButton(true, trueButtonNumber);
        CanvasBomb.instance.UpdateCountDown(timer);
        CanvasBomb.instance.ShowCountDown(true);
        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {
      
        if (countDown)
        {
            timer -= Time.deltaTime;
            CanvasBomb.instance.UpdateCountDown(timer);

            CheckInput();
            //if (timer <= 0f && gameState == GameState.Running)
            //{
            //    Debug.Log("Bomb exploded!");
            //    EndGame();
            //}
        }


    }

   IEnumerator Countdown()
    {
        while(true)
        {
            timer -= Time.deltaTime;
            CanvasBomb.instance.UpdateCountDown(timer);

            CheckInput();
            if (timer <= 0f && gameState == GameState.Running)
            {
                Debug.Log("Bomb exploded!");
                EndGame();
                break;
            }
            yield return null;
        }
       
    }

    private void CheckInput()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit, 50f))
        {
            return;
        }
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
        if (trueButtonNumber == 0)
        {
            WinGame();
        }

    }

    private void WinGame()
    {
        SetGameState(GameState.GameOver);
        CanvasBomb.instance.ShowGameState(true, "Win");
        Time.timeScale = 0f; // Stop the game
    }
    public void SetGameState(GameState state)
    {
        gameState = state;
    }
    private void EndGame()
    {
        SetGameState(GameState.GameOver);
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

