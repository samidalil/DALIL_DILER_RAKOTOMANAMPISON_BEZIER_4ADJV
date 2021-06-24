using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public static DebugManager Instance = null;

    [SerializeField] private float _radius = .05f;
    [SerializeField] private Extrusion _extrusionManager = null;
    [SerializeField] private MeshDisplayer _meshDisplayer = null;
    [SerializeField] private bool _showDebugPlaneTransform = true;
    [SerializeField] private bool _showDebugProfile = true;
    [SerializeField] private bool _showDebugProfileExtruded = true;
    private Vector2[] _F;
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
        if(!Application.isPlaying) return;

        // Display les ptite bouboule !!!!
        if (_showDebugProfileExtruded)
        {
            Gizmos.color = Color.yellow;
            foreach (Vector3 point in this._vertices)
            {
                Gizmos.DrawSphere(point, this._radius);
            }
        }

        if (_showDebugProfile)
        {          
            Gizmos.color = Color.magenta;
            foreach (Vector3 point in _F)
                Gizmos.DrawSphere(point, this._radius);
        }
        
        // Display les repere en chaque A(t)
        if (_showDebugPlaneTransform)
        {
            for (int t = 0; t < _A.Positions.Count; t++)
            {
                Vector3 N = _extrusionManager.GetNormalizedTangent(_A, t);

                Vector3 v = _A.gameObject.transform.right.normalized;
                Vector3 u = AbsV3(Vector3.Cross(N, v)).normalized;

                Gizmos.color = Color.green;
                Gizmos.DrawRay(_A.Positions[t], u);
                Gizmos.color = Color.blue;
                Gizmos.DrawRay(_A.Positions[t], v);
                Gizmos.color = Color.red;
                Gizmos.DrawRay(_A.Positions[t], N);
            }


        }
    }

    public Vector3 AbsV3 (Vector3 v) {
        return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
    }
    public void Extrude(BezierCurve curve)
    {
        Vector2[] profile = new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(1, 0),
            new Vector2(1,1),
        };
        _A = curve;
        _F = profile;
        this._vertices = this._extrusionManager.ExtrudeByPath(profile, curve);
            
        Debug.Log("finished");
        this._meshDisplayer.Display(this._vertices.ToArray(), profile.Length);
    }
}
