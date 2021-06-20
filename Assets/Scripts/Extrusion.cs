using System.Collections.Generic;
using UnityEngine;

public class Extrusion : MonoBehaviour
{
    private Vector3 GetTangent(BezierCurve curve, int n)
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

 
    public List<Vector3> ExtrudeByPath(Vector3[] profile, BezierCurve curve)
    {
        List<Vector3> vertices = new List<Vector3>();
        Vector3 originPoint = Vector3.zero;
        Vector3 normal = Vector3.right;

        for (int i = 0; i < curve.Positions.Count; i++)
        {
            Vector3 tangent = this.GetTangent(curve, i);

            foreach (Vector3 point in profile)
            {
                /**
                 * Il faut orienter le profil en fonction de sa normale d'origine (normal)
                 * et de la tangente au point courant de la courbe de Bézier (tangent)
                 * qui définit la nouvelle normale du plan de contenance du profil 2D
                 * 
                 * là on va rotate tout les points de la forme par rapport à son centre et de l'angle theta
                 * On utilise les coordonées polaire pour cela ( cf. formule changement d'axes de coordonées https://fr.wikipedia.org/wiki/Rotation_plane)
                 */

                //vertices.Add(originPoint + curve.Positions[i] + this.RotatePoint(point, Vector3.SignedAngle(normal, tangent, Vector3.right)));
                vertices.Add(originPoint + curve.Positions[i] + point);
            }
        }

        return vertices;
    }


}