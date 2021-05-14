using System.Collections.Generic;
using UnityEngine;

public class Polygon
{
    #region Variables statiques

    public static List<Polygon> Elements = new List<Polygon>();

    #endregion

    #region Variables d'instance

    public List<Point> Points;

    #endregion

    #region Constructeurs et destructeur

    public Polygon()
    {
        Polygon.Elements.Add(this);
        this.Points = new List<Point>();
    }

    public Polygon(List<Point> points)
    {
        Polygon.Elements.Add(this);
        this.Points = points;
    }

    ~Polygon()
    {
        Polygon.Elements.Remove(this);
    }

    #endregion

    #region Méthodes statiques

    public static void DrawElements()
    {
        foreach (Polygon element in Polygon.Elements)
            element.Draw();
    }

    #endregion

    #region Méthodes publiques

    public virtual void Draw()
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
}
