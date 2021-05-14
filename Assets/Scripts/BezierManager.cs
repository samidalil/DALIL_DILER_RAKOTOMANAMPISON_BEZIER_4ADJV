using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierManager : MonoBehaviour
{
    private bool _isOnUi;

    private int _step = 1;

    private MouseController _mouseController;

    private MenuManager _menuManager;
    #region Singleton

    private static BezierManager _instance;
    private void Awake()
    {
        if (_instance != null)
            Destroy(this);
        else
            _instance = this;
    }

    public static BezierManager Instance => _instance;
    #endregion


    public bool IsOnUi
    {
        get => _isOnUi;
        set => _isOnUi = value;
    }

    public int Step
    {
        get => _step;
        set => _step = value;
    }

    public MouseController MouseController
    {
        get => _mouseController;
        set => _mouseController = value;
    }

    public MenuManager MenuManager
    {
        get => _menuManager;
        set => _menuManager = value;
    }
}
