using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum Mode
{
    NONE = 0 ,
    EDIT = 1 ,
    CREATION = 2
};
public class MouseController : MonoBehaviour
{

    public Camera cam;
    public GameObject bezierMenu;
    public Mode mode;
    public GameObject testSphere;
    private bool _click;

    public void Start()
    {
        switchToNoneMode();
    }


    public void switchToEditMode()
    {
        mode = Mode.EDIT;
    }

    public void switchToNoneMode()
    {
        mode = Mode.NONE;
    }

    public void switchToCreationMode()
    {
        mode = Mode.CREATION;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = Input.mousePosition;
            
            if (mode==Mode.NONE)
            {
                bezierMenu.transform.SetPositionAndRotation(position, Quaternion.identity);
                bezierMenu.SetActive(!bezierMenu.activeSelf);
            }
            else if (mode == Mode.CREATION)
            {
                // Check if the mouse was clicked over a UI element
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    

                    Instantiate(testSphere, cam.ScreenToWorldPoint(position), Quaternion.identity);
                }
            }
            
        }
        
    }
}

