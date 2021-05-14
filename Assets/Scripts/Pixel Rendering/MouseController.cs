using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour
{

    public Camera cam;


    public GameObject testSphere;
    private bool _click;

    public float zIndex;

    


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse was clicked over a UI element
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 position = Input.mousePosition;

                position.z = zIndex;
                Instantiate(testSphere, cam.ScreenToWorldPoint(position), Quaternion.identity);
            }
        }
        
    }
}

