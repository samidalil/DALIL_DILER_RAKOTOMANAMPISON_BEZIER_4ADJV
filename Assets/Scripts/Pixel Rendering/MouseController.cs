using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum Mode
{
    NONE = 0,
    EDIT = 1,
    CREATION = 2
};

public class MouseController : MonoBehaviour
{
    public GameObject pointPrefab;
    private bool _click;
    private MenuManager _menuManager;

    public static MouseController instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    public void Start()
    {
        BezierManager.Instance.MouseController = this;
        _menuManager = BezierManager.Instance.MenuManager;
    }
    

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = Input.mousePosition;

            // if (mode == Mode.NONE)
            //     mousePoint = Input.mousePosition;
            // Check if the mouse was clicked over a UI element
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                _menuManager.setPosOfMenu(position);
            }
            else if (_menuManager.Mode == Mode.CREATION)
            {
                // Check if the mouse was clicked over a UI element
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    Instantiate(pointPrefab, Camera.main.ScreenToWorldPoint(position), Quaternion.identity);
                }
            }
        }
    }
}