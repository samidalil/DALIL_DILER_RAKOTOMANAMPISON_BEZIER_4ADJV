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
    
    #region Variables d'instance

    private int _step = 1;

    private readonly List<BezierCurve> _curves = new List<BezierCurve>();

    #endregion

    #region Propriétés

    public int Step
    {
        get => _step;
        set => _step = value;
    }

    public List<BezierCurve> Curves => this._curves;

    #endregion
}
