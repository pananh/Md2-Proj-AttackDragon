using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UI.CanvasScaler;

public class HealthBar : MonoBehaviour
{
    private float maxHealth;
    private float currentHealth;
    
    public float MaxHealth
    {
        set
        {
            maxHealth = value;
            UpdateHBValue();
        }
    }
    public float CurrentHealth
    {
        set
        {
            currentHealth = Mathf.Clamp(value, 0, maxHealth);
            UpdateHBValue();
        }
    }

    [SerializeField] private UnityEngine.UI.Image emptyBar;
    private GameObject unit;
    private Vector3 unitPos;
    public Vector3 UnitPos     {set { unitPos = value; } }
    private RectTransform hpBarRec;
    private float aboveYPos = 90f;
    private float scaleFactor;

    public void Init(GameObject followUnit, float maxHealth, float currentHealth)
    {
        hpBarRec = GetComponent<RectTransform>();

        unit = followUnit;
        this.maxHealth= maxHealth;
        this.currentHealth = currentHealth;

        Debug.Log("HealthBar Init: " + unit.name + " , Max: " + maxHealth + " , Current: " + currentHealth);

        UpdateHBValue();
        UpdateHBPosition();

    }


    private void Start()
    {
      

    }

    private void LateUpdate()
    {
        if ( unit == null )
        {
            return; // Neu unit bi huy, thi khong can cap nhat nua, giu lai health bar 
        }
        unitPos = unit.transform.position;

        if ( IsHavingChangingCamOrUnit() )
        {
            UpdateHBPosition();
        }
    }

    private void UpdateHBPosition()
    {
        float distance = Vector3.Distance(Camera.main.transform.position, unitPos);
        scaleFactor = Mathf.Clamp(10f / distance, 0.5f, 2.0f);

        hpBarRec.localScale = new Vector3(scaleFactor, scaleFactor, 1f);

        hpBarRec.anchoredPosition = WordToCanvas(unitPos, HealthBarManager.Instance.CanvasRect) 
                                        + new Vector2(0, aboveYPos*scaleFactor);

    }

    private bool IsHavingChangingCamOrUnit()
    {
        return Vector3.SqrMagnitude(unitPos - Camera.main.transform.position) > 0.01f;
    }

    private Vector2 WordToCanvas(Vector3 unitPos, RectTransform canvasRect)
    {
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, unitPos); // Tra ve dang toa do theo do phan giai
        Vector2 localCanvasPos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos,
            HealthBarManager.Instance.Canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main, out localCanvasPos);
        // Tinh theo toa do Local cua Canvas cha, de che do ScreenSpaceOverlay nen can de null Camera, che do khac thi truyen Camera vao
        return localCanvasPos;
    }


    private Vector2 WorldToCanvasByViewport(Vector2 unitPos, RectTransform canvasRect)
    {
        Vector2 viewportPos = Camera.main.WorldToViewportPoint(unitPos); // Tra ve dang toa do ty le tu 0 -> 1
        float canvasWidth = canvasRect.sizeDelta.x;
        float canvasHeight = canvasRect.sizeDelta.y;
        float x = (viewportPos.x - 0.5f) * canvasWidth;     // canvas co toa do o giua, nen can tru di nua
        float y = (viewportPos.y - 0.5f) * canvasHeight;
        return new Vector2(x, y);
    }

    private void UpdateHBValue()
    {
        emptyBar.fillAmount = 1 - currentHealth / maxHealth;
    }
    
}
