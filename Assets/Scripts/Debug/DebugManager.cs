using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public static DebugManager Instance = null;

    [SerializeField] private float _radius = .05f;
    [SerializeField] private Extrusion _extrusionManager = null;
    [SerializeField] private MeshDisplayer _meshDisplayer = null;
    private BezierCurve _A;

    private List<Vector3> _vertices = new List<Vector3>();

    private void Awake()
    {
        DebugManager.Instance = this;
    }

    private void OnDestroy()
    {
        if (DebugManager.Instance == this) DebugManager.Instance = null;
    }

    private void OnDrawGizmos()
    {
        // Gizmos.color = Color.red;
        // foreach (Vector3 point in this._vertices)
        //     Gizmos.DrawSphere(point, this._radius);

        for (int t = 0; t < _A.Positions.Count; t++)
        {
            Vector3 N =  _extrusionManager.GetNormalizedTangent(_A, t);

            Vector3 v = _A.gameObject.transform.right.normalized;
            Vector3 u = Vector3.Cross(N, v);

            Gizmos.color = Color.green;
            Gizmos.DrawRay(_A.Positions[t], u);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(_A.Positions[t], v);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_A.Positions[t], N);
        }
        
        
    }

    public void Extrude(BezierCurve curve)
    {
        Vector3[] profile = new Vector3[]
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 1),
            new Vector3(0, 1, 1),
            new Vector3(0, 1, 0),
        };
        _A = curve;

        this._vertices = this._extrusionManager.ExtrudeByPath(profile, curve);
        Debug.Log("finished");
        this._meshDisplayer.Display(this._vertices.ToArray(), profile.Length);
    }

    /// <summary>
    /// Display the extrusion plan at a point A(t).
    /// It compute N, the plan normal, U and V the base vector of the repere ( A(t), u, v)
    /// </summary>
    /// <param name="curve">Bezier curve</param>
    /// <param name="t">point index</param>
    public void DisplayPlanAtPoint(BezierCurve curve, int t)
    {
        Vector3 N =  _extrusionManager.GetNormalizedTangent(curve, t);
      

    }
}
