using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasBomb : MonoBehaviour
{

    public static CanvasBomb instance;
    [SerializeField] private TextMeshProUGUI countDown;
    [SerializeField] private TextMeshProUGUI trueBotton;
    [SerializeField] private TextMeshProUGUI gameState;

    void Start()
    {
        instance = this;
        countDown.gameObject.SetActive(false);
        gameState.gameObject.SetActive(false); 
        trueBotton.gameObject.SetActive(false);

    }

    public void ShowGameState(bool active, string state)
    {
        gameState.gameObject.SetActive(active);
        gameState.text = state;
    }

    public void ShowCountDown(bool active, float time)
    {
        countDown.gameObject.SetActive(active);
        countDown.text = "Time: " + time.ToString("F2");
    }

    public void ShowTrueButton(bool active, int number)
    {
        trueBotton.gameObject.SetActive(active);
        trueBotton.text = "True Button: " + number.ToString();
    }
}
