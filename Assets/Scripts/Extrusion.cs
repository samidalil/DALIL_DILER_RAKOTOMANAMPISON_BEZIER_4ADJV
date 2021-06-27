using System.Collections.Generic;
using UnityEngine;

public class Extrusion : MonoBehaviour
{
    public Vector3 GetNormalizedTangent(BezierCurve curve, int n)
    {
        Vector3 tangent;

        if (n == 0)
            tangent = curve.Points[1].Position - curve.Points[0].Position;
        else if (n == curve.Positions.Count - 1)
            tangent = curve.Points[curve.Points.Count - 1].Position - curve.Points[curve.Points.Count - 2].Position;
        else
            tangent = curve.Positions[n + 1] - curve.Positions[n - 1];

        return tangent.normalized;
    }

    private float AngleBetween(Vector3 v, Vector3 u, Vector3 axis)
    {
        Vector3 right = Vector3.Cross(axis, u).normalized;
        u = Vector3.Cross(right, axis).normalized;
        return Mathf.Atan2(Vector3.Dot(v, right), Vector3.Dot(v, u)) * Mathf.Rad2Deg;
    }

    private Vector3 RotatePoint(Vector3 point, float theta)
    {
        return new Vector3(
            point.x * Mathf.Cos(theta) + point.y * Mathf.Sin(theta),
            -point.x * Mathf.Cos(theta) + point.y * Mathf.Sin(theta),
            point.z
        );
    }


    // ça serait mieux de mettre en parametre de la fonction deux out
    // l'un pour recuperer les vertices et l'autre pour les normales
    // les normales N se calculent avec : N = P_normal cross (Xf(s)'*v + Yf(s)'*u)
    public List<Vector3> ExtrudeByPath(Vector2[] profile, BezierCurve curve)
    {
        List<Vector3> vertices = new List<Vector3>();
        //   Vector3 originPoint = Vector3.zero;
        // Vector3 P_v = curve.gameObject.transform.right.normalized; // abscisse du repere dans lequelle s'inscrit le plan
        Vector3 P_v = Vector3.forward; // abscisse du repere dans lequelle s'inscrit le plan

        for (int i = 0; i < curve.Positions.Count; i++)
        {
            Vector3 P_normal = this.GetNormalizedTangent(curve, i); // normal au plan P <=> tangente normalise au point A(t) 
            Vector3 P_u = Vector3.Cross(P_normal, P_v).normalized; // ordonnee du repère dans lequelle s'inscrit le plan
            foreach (Vector3 point in profile)
            {
                Vector3 dx = point.x * P_u;
                Vector3 dy = point.y * P_v;

                // sigma(s,t) = A(t) + Xf(s)*v + Yf(s)*k 
                // ou A(t) est le point de la bézier à l'indice t
                // et ( Xf(s), Yf(s) ) sont les coordonee du point F(s) appartenant au profile
                // u et v sont les vecteurs formant la base du repere ( A(t), u , v ) dans laquelle se trouve le plan P
                vertices.Add(curve.Positions[i] + dx + dy);
            }
        }

        return vertices;
    }
}