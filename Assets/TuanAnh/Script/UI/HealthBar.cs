using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth = 40f;
    [SerializeField] private UnityEngine.UI.Image hbLeft;
    [SerializeField] private UnityEngine.UI.Image hbRight;
    [SerializeField] private GameObject unit;
    //private float halfCanvasWidth = 1920f / 2; 
    //private float halfcanvasHeight = 1080f / 2;
    private Vector2 viewportPos;
    private RectTransform healthBarPos;

    public void ChangeHealth(int health)
    {
        currentHealth = Mathf.Clamp(currentHealth + health, 0, maxHealth);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float healthPercentage = currentHealth / maxHealth;
        hbLeft.fillAmount = healthPercentage;
        hbRight.fillAmount = 1 - healthPercentage;

    }

    private void Start()
    {
        healthBarPos = GetComponent<RectTransform>();
        Debug.Log("Canvas Rect: " + healthBarPos.name);
        UpdateHealthBar();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ChangeHealth(-10); // Decrease health by 10 when H is pressed
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            ChangeHealth(10); // Increase health by 10 when J is pressed
        }

        

        Debug.Log("Unit in World: " + unit.transform.position);

        viewportPos = Camera.main.WorldToViewportPoint(unit.transform.position);

        Canvas canvas = GetComponentInParent<Canvas>();
        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();

        Vector2 screenPosition = new Vector2(
          (viewportPos.x * canvasRectTransform.sizeDelta.x) - (canvasRectTransform.sizeDelta.x * 0.5f),
          (viewportPos.y * canvasRectTransform.sizeDelta.y) - (canvasRectTransform.sizeDelta.y * 0.5f));

        healthBarPos.anchoredPosition = screenPosition;

        //Debug.Log("Unit in Viewport: " + viewportPos);


        //Debug.Log("Screen Width: " + Screen.width + " Screen Height " + Screen.height);

        //Vector2 anchoredPosition = new Vector2(viewportPos.x * Screen.width, viewportPos.y * Screen.height);

        //anchoredPosition.x -= Screen.width / 2; // Centering the health bar
        //anchoredPosition.y -= Screen.height / 2; // Centering the health bar
        //healthBarPos.anchoredPosition = anchoredPosition;

        //Debug.Log("Anchored Position: " + healthBarPos.position);


    }

}
