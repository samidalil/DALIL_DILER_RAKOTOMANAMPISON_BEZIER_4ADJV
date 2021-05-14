using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BezierCurve : MonoBehaviour
{
    #region Variables Unity

    [SerializeField] private Polygon _controlPolygon;
    [Range(1, 200)] [SerializeField] private int _step = 30;

    #endregion

    #region Variables d'instance

    private List<Vector3> _positions = new List<Vector3>();

    #endregion

    #region Proprietes

    public Polygon ControlPolygon
    {
        get => this._controlPolygon;
        set
        {
            if (this._controlPolygon != null)
            {
                this._controlPolygon.Off("PositionChange", this.ComputeCurvePoints);
                this._controlPolygon.Off("NewPoints", this.ComputeCurvePoints);
            }

            this._controlPolygon = value;

            if (this._controlPolygon != null)
            {
                this._controlPolygon.On("PositionChange", this.ComputeCurvePoints);
                this._controlPolygon.On("NewPoints", this.ComputeCurvePoints);
            }
        }
    }

    #endregion

    #region Foncitons Unity

    private void Update()
    {
        this.ComputeCurvePoints();
    }

    private void OnDrawGizmos()
    {
        if (this._positions.Count > 1)
        {
            Gizmos.color = Color.white;

            for (int i = 0; i < this._positions.Count - 1; i++)
                Gizmos.DrawLine(this._positions[i], this._positions[i + 1]);
        }
    }

    #endregion

    #region Fonctions privées

    private void ComputeCurvePoints()
    {
        List<Point> points = this._controlPolygon.Points;
        Vector3[,] arr = new Vector3[points.Count, points.Count];
        this._positions.Clear();

        for (int i = 0; i < points.Count; i++)
            arr[i, 0] = points[i].Position;

        for (int n = 0; n <= this._step; n++)
        {
            float t = (float) n / this._step;

            if (n == 0)
                this._positions.Add(points[0].Position);
            else if (n == this._step)
                this._positions.Add(points[points.Count - 1].Position);
            else
            {
                for (int j = 1; j < points.Count; j++)
                    for (int i = 0; i < points.Count - j; i++)
                       arr[i, j] = (1 - t) * arr[i, j - 1] + t * arr[i + 1, j - 1];

                this._positions.Add(arr[0, points.Count - 1]);
            }
        }
    }

    #endregion
}
