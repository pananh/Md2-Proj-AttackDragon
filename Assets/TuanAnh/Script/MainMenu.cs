using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Scene 2");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game has been closed.");
    }


}
