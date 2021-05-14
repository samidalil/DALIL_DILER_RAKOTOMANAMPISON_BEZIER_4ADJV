using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour
{

    public GameObject pointPrefab;
    private bool _click;

    public float zIndex;

    private Vector3 mousePoint;
    private bool editMode = false;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePoint = Input.mousePosition;
            mousePoint.z = zIndex;
            // Check if the mouse was clicked over a UI element
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (!editMode)
                {
                    Vector3 position = Input.mousePosition;

                    position.z = zIndex;
                    Instantiate(pointPrefab, Camera.main.ScreenToWorldPoint(position), Quaternion.identity);
                }
            }
        }
        
    }
}

