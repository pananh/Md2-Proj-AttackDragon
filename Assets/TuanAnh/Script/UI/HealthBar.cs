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
    private Vector3 unitLastPos;
    private Canvas canvas;
    private RectTransform canvasRec;
    private RectTransform healthBarRec;
    private const float aboveYPos = 100f; // Height of the health bar in pixels


    private void Start()
    {
        UpdateHealthBar();
        canvas = GetComponentInParent<Canvas>();
        canvasRec = canvas.GetComponent<RectTransform>();
        healthBarRec = GetComponent<RectTransform>();
        unitLastPos = unit.transform.position;


    }

    private void Update()
    {
        ChangeHealthByKey();
      
    }

    private void LateUpdate()
    {
        if (IsUnitNotMove())
        {
            return; // If the unit has not moved, skip the update
        }
        healthBarRec.anchoredPosition = WorldToCanvasUsingLocalPoint(unit.transform.position, canvasRec);
    }

    private bool IsUnitNotMove()
    {
        if (Vector3.SqrMagnitude(unit.transform.position - unitLastPos) < 0.1f)
        {
            unitLastPos = unit.transform.position;
            return true;
        }
        return false;
    }

    private Vector2 WorldToCanvasUsingLocalPoint(Vector3 unitPos, RectTransform canvasRect)
    {
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, unitPos); // Tra ve dang toa do theo do phan giai
        Vector2 localCanvasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, 
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main, out localCanvasPos);
        // Tinh theo toa do Local cua Canvas cha, de che do ScreenSpaceOverlay nen can de null Camera, che do khac thi truyen Camera vao
        localCanvasPos.y += aboveYPos;
        return localCanvasPos;
    }


    private Vector2 WorldToCanvas(Vector2 unitPos, RectTransform canvasRect)
    {
        Vector2 viewportPos = Camera.main.WorldToViewportPoint(unitPos); // Tra ve dang toa do ty le tu 0 -> 1
        float canvasWidth = canvasRect.sizeDelta.x;
        float canvasHeight = canvasRect.sizeDelta.y;
        float x = (viewportPos.x - 0.5f) * canvasWidth;     // canvas co toa do o giua, nen can tru di nua
        float y = (viewportPos.y - 0.5f) * canvasHeight + aboveYPos;
        return new Vector2(x, y);
    }
    public void TakeHealth(int health)
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

    private void ChangeHealthByKey()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeHealth(-10); // Decrease health by 10 when H is pressed
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeHealth(10); // Increase health by 10 when J is pressed
        }

    }

}
