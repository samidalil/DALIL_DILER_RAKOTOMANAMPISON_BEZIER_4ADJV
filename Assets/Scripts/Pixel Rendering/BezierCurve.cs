using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    #region Variables Unity

    [SerializeField] private LineRenderer _lineRenderer = null;

    #endregion

    #region Variables d'instance

    public List<Point> Points = new List<Point>();

    public List<Vector3> Positions = new List<Vector3>();

    #endregion

    #region Méthodes publiques

    public void FixedUpdate()
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

    private void ComputeCurvePoints()
    {
        List<Point> points = this.Points;
        Vector3[,] arr = new Vector3[points.Count, points.Count];
        this.Positions.Clear();

        if (points.Count == 0) return;

        for (int i = 0; i < points.Count; i++)
            arr[i, 0] = points[i].Position;

        for (int n = 0; n <= BezierManager.Instance.Step; n++)
        {
            float t = (float) n / BezierManager.Instance.Step;

            if (n == 0)
                this.Positions.Add(points[0].Position);
            else if (n == BezierManager.Instance.Step)
                this.Positions.Add(points[points.Count - 1].Position);
            else
            {
                for (int j = 1; j < points.Count; j++)
                    for (int i = 0; i < points.Count - j; i++)
                       arr[i, j] = (1 - t) * arr[i, j - 1] + t * arr[i + 1, j - 1];

                this.Positions.Add(arr[0, points.Count - 1]);
            }
        }
    }

    #endregion
}
