using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public static DebugManager Instance = null;

    [SerializeField] private float _radius = .05f;
    [SerializeField] private Extrusion _extrusionManager = null;
    [SerializeField] private MeshDisplayer _meshDisplayer = null;
    [SerializeField] private bool _showDebugProfileExtruded = true;
    [SerializeField] private bool _showDebugPlaneTransform = true;

    private BezierCurve _curve = null;
    private List<Vector3> _vertices = new List<Vector3>();
    private List<Vector3> _normales = new List<Vector3>();

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
        if(!Application.isPlaying) return;

        if (this._showDebugProfileExtruded)
        {
            Gizmos.color = Color.yellow;
            foreach (Vector3 point in this._vertices)
                Gizmos.DrawSphere(point, this._radius);
        }

        if (this._showDebugPlaneTransform && this._curve != null)
        {
            for (int t = 0; t < this._curve.Positions.Count; t++)
            {
                Vector3 N = this._extrusionManager.GetNormalizedTangent(this._curve, t);
                Vector3 v = this._curve.gameObject.transform.right.normalized;
                Vector3 u = AbsV3(Vector3.Cross(N, v)).normalized;

                Gizmos.color = Color.green;
                Gizmos.DrawRay(this._curve.Positions[t], u);
                Gizmos.color = Color.blue;
                Gizmos.DrawRay(this._curve.Positions[t], v);
                Gizmos.color = Color.red;
                Gizmos.DrawRay(this._curve.Positions[t], N);
            }
        }
    }

    public Vector3 AbsV3(Vector3 v)
    {
        return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
    }

    public void Extrude(BezierCurve curve, Vector2[] profile)
    {
        this._curve = curve;
        this._extrusionManager.ExtrudeByPath(profile, curve, out this._vertices, out this._normales);
        this._meshDisplayer.Display(this._vertices.ToArray(), this._normales.ToArray(), profile.Length);
    }
}
