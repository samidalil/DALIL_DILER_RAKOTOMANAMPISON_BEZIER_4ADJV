using System.Collections.Generic;
using UnityEngine;

public class BezierCurve
{
    #region Variables Unity

    public static int Step = 30;

    public static List<BezierCurve> Elements = new List<BezierCurve>();

    #endregion

    #region Variables d'instance

    public Polygon ControlPolygon;

    public List<Vector3> Positions = new List<Vector3>();

    #endregion

    #region Constructeur
    
    public BezierCurve(Polygon controlPolygon)
    {
        BezierCurve.Elements.Add(this);
        this.ControlPolygon = controlPolygon;
    }

    ~BezierCurve()
    {
        BezierCurve.Elements.Remove(this);
    }

    #endregion

    #region Méthodes statiques

    public static void DrawElements()
    {
        foreach (BezierCurve element in BezierCurve.Elements)
            element.Draw();
    }

    #endregion

    #region Méthodes publiques

    public void Draws()
    {
        this.ComputeCurvePoints();
    }

    public void Draw()
    {
        if (this.Positions.Count > 1)
        {
            Gizmos.color = Color.white;

            for (int i = 0; i < this.Positions.Count - 1; i++)
                Gizmos.DrawLine(this.Positions[i], this.Positions[i + 1]);
        }
    }

    #endregion

    #region Méthodes privées

    private void ComputeCurvePoints()
    {
        List<Point> points = this.ControlPolygon.Points;
        Vector3[,] arr = new Vector3[points.Count, points.Count];
        this.Positions.Clear();

        for (int i = 0; i < points.Count; i++)
            arr[i, 0] = points[i].Position;

        for (int n = 0; n <= BezierCurve.Step; n++)
        {
            float t = (float) n / BezierCurve.Step;

            if (n == 0)
                this.Positions.Add(points[0].Position);
            else if (n == BezierCurve.Step)
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
