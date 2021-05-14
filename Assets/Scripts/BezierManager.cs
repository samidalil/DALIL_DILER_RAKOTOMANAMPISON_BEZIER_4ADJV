using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierManager : MonoBehaviour
{

    private bool _isOnUi;

    private int _step = 1;
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
}
