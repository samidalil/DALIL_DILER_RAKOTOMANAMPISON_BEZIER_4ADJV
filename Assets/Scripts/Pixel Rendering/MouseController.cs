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

                    BezierManager.Instance.CreatePointInCurve(position);
                    if (BezierManager.Instance.CurrentCurve.Points.Count == BezierManager.Instance.CurrentCurve.Degree + 1)
                    {
                        BezierManager.Instance.MenuManager.SwitchToEditMode();
                        ProfileMenuManager.Instance.ActivateMenu();
                    }
                }
                else if (_menuManager.Mode == Mode.PROFILECREATION)
                {
                    position.z = 2;
                    BezierManager.Instance.CreatePointProfile(position);
                    if (BezierManager.Instance.CurrentProfile.Count == BezierManager.Instance.ProfilePointNumber)
                    {
                        if (BezierManager.Instance.ProfileMenuManager.ToggleVal)
                            BezierManager.Instance.CurrentProfile.Add(BezierManager.Instance.CurrentProfile[0]);

                        foreach (Point p in BezierManager.Instance.ProfilePoints)
                            GameObject.Destroy(p.gameObject);
                        BezierManager.Instance.ProfilePoints.Clear();
                        BezierManager.Instance.MenuManager.SwitchToEditMode();
                        BezierManager.Instance.CurrentProfile.Add(BezierManager.Instance.CurrentProfile[0]);
                        DebugManager.Instance.Extrude(BezierManager.Instance.CurrentCurve, BezierManager.Instance.CurrentProfile.ToArray());
                        BezierManager.Instance.CurrentCurve.OnRecompute += () => DebugManager.Instance.Extrude(BezierManager.Instance.CurrentCurve, BezierManager.Instance.CurrentProfile.ToArray());
                    }
                }
                else
                {
                    Ray ray = Camera.main.ScreenPointToRay(position);
                    RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
                    
                    if(hit.collider == null)
                    {
                        _menuManager.SetPosOfMenu(position);
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
                        
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 position = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(position);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
                if(hit.collider != null)
                {
                    Debug.Log("hit " + hit.transform.gameObject.name);
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Curve"))
                    {
                        BezierManager.Instance.EditMenuManager.TargetEdit = hit.transform.gameObject;
                        BezierManager.Instance.EditMenuManager.SetPosOfMenu(position);
                    }
                }
            }
        }
    }
}