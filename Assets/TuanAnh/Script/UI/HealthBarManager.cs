using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    public static HealthBarManager Instance { get; private set;  }

    private Canvas canvas;
    public Canvas Canvas => canvas;
    private RectTransform canvasRect;
    public RectTransform CanvasRect => canvasRect;


    private List<HealthBar> healthBarList;
    [SerializeField] List<GameObject> gameObjects;

    private void Awake()
    {
        Instance = this;
        canvas = GetComponent<Canvas>();
        canvasRect = GetComponent<RectTransform>();
    }

    void Start()
    {
        



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
