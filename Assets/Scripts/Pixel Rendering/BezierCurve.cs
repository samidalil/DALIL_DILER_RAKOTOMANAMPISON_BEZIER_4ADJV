using System;
using System.Collections.Generic;
using UnityEngine;

public enum ExtendStrategy
{
    Continu,
    C1,
    C2,
}

public class BezierCurve : MonoBehaviour
{
    #region Evènements

    public event Action OnRecompute;

    #endregion

    #region Variables Unity

    [SerializeField] private LineRenderer _lineRenderer = null;
    [SerializeField] private LineRenderer _lineRendererHull = null;
    [SerializeField] private PolygonCollider2D _polygonCollider2D = null;

    #endregion

    #region Variables d'instance

    public int Degree = 3;

    public int Step = 10; //TODO IMPLEMENT

    public List<Point> Points = new List<Point>();
    
    public List<Vector3> ConvexHull = new List<Vector3>();

    public List<Vector2> ConvexHull2D = new List<Vector2>();
    
    public List<Vector3> Positions = new List<Vector3>();

    #endregion

    #region Méthodes publiques

    public void AddPoint(Point p, int i = -1)
    {
        if (i < 0) this.Points.Add(p);
        else this.Points.Insert(i, p);

        p.OnPositionChanged += this.Recompute;
        this.Recompute();
    }

    public void RemovePoint(Point p)
    {
        this.Points.Remove(p);
        p.OnPositionChanged -= this.Recompute;
        this.Recompute();
    }

    public void Recompute()
    {
        this.DrawCurve();
        this.ComputeConvexHull();

        if (this.ConvexHull.Count > 1)
        {
            this._lineRendererHull.SetPositions(this.ConvexHull.ToArray());
            this._lineRendererHull.positionCount = this.ConvexHull.Count;
                
            this.ConvexHull2D.Clear();
            foreach (Vector3 v in this.ConvexHull)
                this.ConvexHull2D.Add((Vector2)v);
            this._polygonCollider2D.SetPath(0, this.ConvexHull2D);
        }

        this.OnRecompute?.Invoke();
    }

    public void DrawCurve()
    {
        this.ComputeCurvePoints();
        
        if (this.Positions.Count > 1)
        {
            this._lineRenderer.SetPositions(this.Positions.ToArray());
            this._lineRenderer.positionCount = this.Positions.Count;
        }

    }
    #endregion

    #region Méthodes privées

    // De Casteljau

    private void ComputeCurvePoints()
    {
        this.Positions.Clear();
        
        if (this.Points.Count == 0) return;
        
        Vector3[,] arr = new Vector3[this.Points.Count, this.Points.Count];

        for (int i = 0; i < this.Points.Count; i++)
            arr[i, 0] = this.Points[i].Position;

        for (int n = 0; n <= Step; n++)
        {
            float t = (float) n / Step;

            if (n == 0)
                this.Positions.Add(this.Points[0].Position);
            else if (n == Step)
                this.Positions.Add(this.Points[this.Points.Count - 1].Position);
            else
            {
                for (int j = 1; j < this.Points.Count; j++)
                    for (int i = 0; i < this.Points.Count - j; i++)
                       arr[i, j] = (1 - t) * arr[i, j - 1] + t * arr[i + 1, j - 1];

                this.Positions.Add(arr[0, this.Points.Count - 1]);
            }
        }
    }

    // Marche de Jarvis

    private Point GetLeftPoint()
    {
        Point p = this.Points[0];

        foreach (Point point in this.Points) if (point.Position.x < p.Position.x) p = point;
        return p;
    }

    private static float Det(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return Mathf.Sign((p2.x - p1.x) * (p3.y - p1.y) - (p3.x - p1.x) * (p2.y - p1.y));
    }

    private void ComputeConvexHull()
    {
        this.ConvexHull.Clear();

        if (this.Points.Count == 0) return;
        
        List<Point> hull = new List<Point>();
        Point pointOnHull = this.GetLeftPoint();
        Point endpoint;

        do
        {
            hull.Add(pointOnHull);
            endpoint = this.Points[0];

            foreach (Point p in this.Points)
                if (endpoint == pointOnHull || BezierCurve.Det(pointOnHull.Position, endpoint.Position, p.Position) < 0)
                    endpoint = p;

            pointOnHull = endpoint;
        }
        while (endpoint != hull[0]);

        foreach (Point p in hull)
            this.ConvexHull.Add(p.Position);
        this.ConvexHull.Add(hull[0].Position);
    }

    #endregion
}
