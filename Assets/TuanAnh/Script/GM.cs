using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    public static GM Instance { get; private set; }

    [SerializeField] private GameObject gameOverObject;
    [SerializeField] private GameObject gamePauseObject;
    private bool isPaused;

    void Awake()
    {
       Instance = this;
    }

    void Start()
    {
        EnterGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        } 
    }

    


    public void EnterGame()
    {
        PlayerController.Instance.Init();
        MonsterManager.Instance.Init();

        UIMinimap.Instance.Init();
        HealthBarManager.Instance.Init();
        SoundManager.Instance.Init();

        gameOverObject.SetActive(false);
        gamePauseObject.SetActive(false);
        isPaused = false;
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f; // Reset time scale to normal

        PlayerController.Instance.ResetInstance();
        MonsterManager.Instance.ResetInstance();
        UIMinimap.Instance.ResetInstance();
        HealthBarManager.Instance.ResetInstance();
        SoundManager.Instance.ResetInstance();
        SceneManager.LoadScene("GameMenu");
    }

    public void GameOver()
    {
        SoundManager.Instance.StopAllSounds();
        
        StartCoroutine(ShowGameOverAfterDelay(5f)); // Show game over after 1 second delay
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        gamePauseObject.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        gamePauseObject.SetActive(false);
    }

    private IEnumerator ShowGameOverAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameOverObject.SetActive(true);
        Time.timeScale = 0f;
    }

}
