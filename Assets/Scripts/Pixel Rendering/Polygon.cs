using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Polygon : Emitter
{
    #region Variables Unity

    [SerializeField]
    private List<Point> _points;

    #endregion

    #region Propriétés

    public List<Point> Points
    {
        get => this._points;
        set
        {
            if (this._points != null)
                foreach (Point p in this._points)
                    p.Off("PositionChange", this.OnPositionChange);

            this._points = value;

            if (this._points != null)
                foreach (Point p in this._points)
                    p.On("PositionChange", this.OnPositionChange);

            this.Emit("NewPoints");
        }
    }

    #endregion

    #region Fonctions Unity

    private void OnDrawGizmos()
    {
        if (this.Points.Count > 2)
        {
            Gizmos.color = Color.black;

            for (int i = 0; i < this.Points.Count - 1; i++)
                Gizmos.DrawLine(this.Points[i].Position, this.Points[i + 1].Position);
            Gizmos.DrawLine(this.Points[this.Points.Count - 1].Position, this.Points[0].Position);
        }
    }

    #endregion

    #region Fonctions privées

    private void OnPositionChange()
    {
        this.Emit("PositionChange");
    }

    #endregion
}
