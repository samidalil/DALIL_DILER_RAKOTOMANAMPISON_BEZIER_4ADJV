using System.Collections.Generic;
using UnityEngine;

public class BezierManager : MonoBehaviour
{
    #region Singleton

    private static BezierManager _instance;
    
    private void Awake()
    {
        if (BezierManager._instance != null) Destroy(this);
        else BezierManager._instance = this;
    }

    private void OnDestroy()
    {
        if (BezierManager._instance == this) BezierManager._instance = null;
    }

    public static BezierManager Instance => BezierManager._instance;

    #endregion

    #region Variables Unity

    [SerializeField] private GameObject _curvePrefab = null;

    [SerializeField] private GameObject _pointPrefab = null;

    #endregion

    #region Variables d'instance

    private int _step = 50;

    private BezierCurve _currentCurve = null;

    private MouseController _mouseController;

    private MenuManager _menuManager;

    private EditMenuManager _editMenuManager;

    public EditMenuManager EditMenuManager
    {
        get => _editMenuManager;
        set => _editMenuManager = value;
    }

    private readonly List<BezierCurve> _curves = new List<BezierCurve>();

    #endregion

    #region Propriétés

    public int Step
    {
        get => _step;
        set => _step = value;
    }

    public BezierCurve CurrentCurve
    {
        get => this._currentCurve;
        set => this._currentCurve = value;
    }

    public GameObject CurvePrefab => this._curvePrefab;
    
    public GameObject PointPrefab => this._pointPrefab;

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

    public List<BezierCurve> Curves => this._curves;

    #endregion
}
