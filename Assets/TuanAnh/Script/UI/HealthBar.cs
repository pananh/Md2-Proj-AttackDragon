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
    private float health;
    
    public float MaxHealth
    {
        set
        {
            maxHealth = value;
            UpdateHBValue();
        }
    }
    public float Health
    {
        set
        {
            health = Mathf.Clamp(value, 0, maxHealth);
            UpdateHBValue();
        }
    }

    private UnityEngine.UI.Image emptyBar;
    private Vector3 unitPos;
    public Vector3 UnitPos     {set { unitPos = value; } }
    private Vector3 unitLastPos;
    private float lastCameraOrthographicSize;
    private RectTransform healthBarRec;
    private float aboveYPos = 90f;
    private float scaleFactor;

    private void Start()
    {
        emptyBar = GetComponentInChildren<UnityEngine.UI.Image>();
        healthBarRec = GetComponent<RectTransform>();

        unitPos = PlayerController.Instance.transform.position;

        unitLastPos = unitPos;
        lastCameraOrthographicSize = Camera.main.orthographicSize;
        UpdateHBPosition();

        maxHealth = 100f;
        health = 30f;

        UpdateHBValue();
    }

    private void LateUpdate()
    {
        unitPos = PlayerController.Instance.transform.position;
        if (IsUnitMove() || IsCameraZoomed())
        {
            UpdateHBPosition();
        }
    }

    private void UpdateHBPosition()
    {
        float distance = Vector3.Distance(Camera.main.transform.position, unitPos);
        scaleFactor = Mathf.Clamp(10f / distance, 0.5f, 2.0f);

        healthBarRec.localScale = new Vector3(scaleFactor, scaleFactor, 1f);

        healthBarRec.anchoredPosition = WordToCanvas(unitPos, HealthBarManager.Instance.CanvasRect) 
                                        + new Vector2(0, aboveYPos*scaleFactor);

    }

    private bool IsCameraZoomed()
    {
        if (Mathf.Abs(Camera.main.orthographicSize - lastCameraOrthographicSize) > 0.01f)
        {
            lastCameraOrthographicSize = Camera.main.orthographicSize;
            return true;
        }
        return false;
    }

    private bool IsUnitMove()
    {
        if (Vector3.SqrMagnitude(unitPos - unitLastPos) > 0.1f)
        {
            unitLastPos = unitPos;
            return true;
        }
        return false;
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
        emptyBar.fillAmount = 1 - health / maxHealth;
    }
    
}
