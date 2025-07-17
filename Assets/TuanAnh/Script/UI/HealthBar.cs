using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UI.CanvasScaler;

public class HealthBar : MonoBehaviour
{
    private float maxHealth;
    private float currentHealth;

    [SerializeField] private UnityEngine.UI.Image emptyBar;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI aboveText;
    private Transform unitTransform;
    private Vector3 unitPos;
    private RectTransform hpBarRec;
    private float aboveYPos = 90f;
    private float scaleFactor;

    private void OnEnable()
    {
        aboveText.gameObject.SetActive(false);
        maxHealth = 100f;
        currentHealth = 100f;
        emptyBar.fillAmount = 0f;
    }


    public void SetPosition(Transform unitTranformInput)
    {
        hpBarRec = GetComponent<RectTransform>();
        unitTransform = unitTranformInput;
        UpdateHBPosition();
        
    }

    public void SetHealthData(float currentHealth, float maxHealth)
    {
        this.currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        this.maxHealth = maxHealth;
        UpdateHealthBarImage();
    }

    public void SetHealthData(float currentHealth)
    {
        this.currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBarImage();
    }

    public void SetLevel(float level)
    {
        levelText.text =level.ToString();
    }

    public void SetAboveText(string text)
    {
        if (!aboveText.gameObject.activeSelf)
            aboveText.gameObject.SetActive(true);
        aboveText.text = text;

    }

    private void LateUpdate()
    {
        if ( unitTransform == null )
        {
            return; // Neu unit bi huy, thi khong can cap nhat nua, giu lai health bar 
        }
        unitPos = unitTransform.position;

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
                                        + new Vector2(0, Mathf.Clamp(aboveYPos*scaleFactor,40f, 120f));
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

    private void UpdateHealthBarImage()
    {
        if (currentHealth <= 0)
        {
            emptyBar.fillAmount = 1; 
            return;
        }
        emptyBar.fillAmount = 1 - currentHealth / maxHealth;
    }
    
}
