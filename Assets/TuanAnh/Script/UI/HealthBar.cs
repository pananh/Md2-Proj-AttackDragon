using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth = 40f;
    [SerializeField] private UnityEngine.UI.Image hbLeft;
    [SerializeField] private UnityEngine.UI.Image hbRight;
    [SerializeField] private GameObject unit;
    private float halfCanvasWidth = 1920f / 2; 
    private float halfcanvasHeight = 1080f / 2;
    private Vector2 ViewportPosition;
    private RectTransform CanvasRect;

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
        CanvasRect = GetComponent<RectTransform>();
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

        ViewportPosition = Camera.main.WorldToViewportPoint(unit.transform.position);

        //Debug.Log("Object: " + unit.name + " " + unit.transform.position + " => " + vector2);

        this.CanvasRect.anchoredPosition = new Vector2(
            (ViewportPosition.x * 1920f) - halfCanvasWidth,
            (ViewportPosition.y * 1080f) - halfcanvasHeight
        );


    }

}
