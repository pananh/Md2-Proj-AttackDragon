using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class IUMinimap : MonoBehaviour
{
    [SerializeField] Image playerIcon;
    [SerializeField] MonoBehaviour controller;
    private IUnitController IUnitControllerReturn
    {
        get { return controller as IUnitController; }
    }
    
    [SerializeField] RectTransform miniMap;
    [SerializeField] Vector2 worldMin;  // 0.0
    [SerializeField] Vector2 worldMax;  // 200.200

    private CharacterController characterController;
    private Vector3 playerWorldPosition; 
    private float xNorm, yNorm, xPos, yPos;


    void Start()
    {
        playerIcon.enabled = true;
        characterController = IUnitControllerReturn.GetCharacterController;
    }

    void LateUpdate()
    {
        playerWorldPosition = characterController.transform.position;

        xNorm = Mathf.InverseLerp(worldMin.x, worldMax.x, playerWorldPosition.x);
        yNorm = Mathf.InverseLerp(worldMin.y, worldMax.y, playerWorldPosition.z); 

        xPos = Mathf.Lerp(0, miniMap.rect.width, xNorm);
        yPos = Mathf.Lerp(0, miniMap.rect.height, yNorm);

        playerIcon.rectTransform.anchoredPosition = new Vector2(xPos, yPos);

    }
}


