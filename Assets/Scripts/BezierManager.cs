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

    #region Méthodes publiques

    public Point CreatePointInCurve(Vector3 position)
    {
        Point point = GameObject.Instantiate(
            this._pointPrefab,
            Camera.main.ScreenToWorldPoint(position),
            Quaternion.identity
        ).GetComponent<Point>();

        point.transform.SetParent(this._currentCurve.transform);
        this._currentCurve.AddPoint(point);

        return point;
    }

    public BezierCurve CreateCurve(int degree)
    {
        this._currentCurve = GameObject.Instantiate(
            this._curvePrefab,
            Vector3.zero,
            Quaternion.identity
        ).GetComponent<BezierCurve>();

        this._currentCurve.Degree = degree;

        return this._currentCurve;
    }

    public void ExtendCurve(BezierCurve originCurve, BezierCurve newCurve, ExtendStrategy strategy)
    {
        Point pn = originCurve.Points[originCurve.Points.Count - 1];
        this.CreatePointInCurve(originCurve.Points[originCurve.Points.Count - 1].Position);

        if (strategy == ExtendStrategy.Continu) return;
        Point pnm1 = originCurve.Points[originCurve.Points.Count - 2];
        Point p1 = this.CreatePointInCurve(2 * pn.Position - pnm1.Position);
        
        if (strategy == ExtendStrategy.C1 || originCurve.Degree == 1) return;
        this.CreatePointInCurve(originCurve.Points[originCurve.Points.Count - 2].Position + 2 * (p1.Position - pnm1.Position));
    }

    #endregion
}
