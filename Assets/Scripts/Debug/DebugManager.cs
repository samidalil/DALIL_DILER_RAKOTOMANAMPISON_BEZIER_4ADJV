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
    }

    public void Extrude(BezierCurve curve, Vector2[] profile)
    {
        /*Vector2[] profile = new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(0, 1),
            new Vector2(-1, 1),
            new Vector2(-1, 0),
        };*/

        this._vertices = this._extrusionManager.ExtrudeByPath(profile, curve);
        this._meshDisplayer.Display(this._vertices.ToArray(), profile.Length);
    }
}
