using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class UIMinimap : MonoBehaviour
{
    [SerializeField] Image playerIcon;
    [SerializeField] GameObject playerObject;

    // Khong dung kieu nay nua vi da dung gameObject
    //[SerializeField] MonoBehaviour controller;
    //private IUnitController IUnitControllerReturn
    //{
    //    get { return controller as IUnitController; }
    //}

    [SerializeField] Image monsterIcon;
    [SerializeField] GameObject monsterObject;

    [SerializeField] RectTransform miniMap;
    [SerializeField] Vector2 worldMin;  // 0.0
    [SerializeField] Vector2 worldMax;  // 200.200

    void Start()
    {
        playerIcon.enabled = true;
        monsterIcon.enabled = true;
        //characterController = IUnitControllerReturn.GetCharacterController;
    }

    void LateUpdate()
    {
        
        SetIconLocation(playerObject, playerIcon);
        SetIconLocation(monsterObject, monsterIcon);

    }

    private void SetIconLocation(GameObject sourceObject, Image iconImage)
    {
        Vector3 worldPosition = sourceObject.transform.position;
        float xNorm = Mathf.InverseLerp(worldMin.x, worldMax.x, worldPosition.x);
        float yNorm = Mathf.InverseLerp(worldMin.y, worldMax.y, worldPosition.z);
        float xPos = Mathf.Lerp(0, miniMap.rect.width, xNorm);
        float yPos = Mathf.Lerp(0, miniMap.rect.height, yNorm);
        iconImage.rectTransform.anchoredPosition = new Vector2(xPos, yPos);
    }

}

