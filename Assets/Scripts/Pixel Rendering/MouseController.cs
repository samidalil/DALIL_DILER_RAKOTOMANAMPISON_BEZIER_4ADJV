using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class MouseController : MonoBehaviour
{
    private MenuManager _menuManager;

    private static MouseController instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
        BezierManager.Instance.MouseController = this;
    }

    public void Start()
    {
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
                // Check if the mouse was clicked over a UI element
                /*if (_menuManager.Mode == Mode.NONE)
                {
                    _menuManager.setPosOfMenu(position);
                }
                else if (_menuManager.Mode == Mode.EDIT)
                {
                    // BOUGER LES POINTS ETC
                }
                else */
                if (_menuManager.Mode == Mode.CREATION)
                {
                    position.z = 2;

                    Point point = Instantiate(BezierManager.Instance.PointPrefab, Camera.main.ScreenToWorldPoint(position), Quaternion.identity).GetComponent<Point>();

                    point.transform.SetParent(BezierManager.Instance.CurrentCurve.transform);
                    BezierManager.Instance.CurrentCurve.AddPoint(point);
                    if (BezierManager.Instance.CurrentCurve.Points.Count ==
                        BezierManager.Instance.CurrentCurve.Degree + 1)
                    {
                        BezierManager.Instance.MenuManager.SwitchToEditMode();
                        Debug.Log("switching to edit");
                    }
                }
                else
                {
                    _menuManager.SetPosOfMenu(position);
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
                        
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 position = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(position);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit, 100.0f))
                {
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Curve"))
                    {
                        BezierManager.Instance.EditMenuManager.TargetEdit = hit.transform.gameObject;
                        Debug.Log("clicked on curve");
                        BezierManager.Instance.EditMenuManager.SetPosOfMenu(position);
                    }
                }
            }
        }
    }
}