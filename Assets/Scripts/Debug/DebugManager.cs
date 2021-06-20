using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public static DebugManager Instance = null;

    [SerializeField] private float _radius = .05f;
    [SerializeField] private Extrusion _extrusionManager = null;
    [SerializeField] private MeshDisplayer _meshDisplayer = null;

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
        Gizmos.color = Color.red;
        foreach (Vector3 point in this._vertices)
            Gizmos.DrawSphere(point, this._radius);
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

        this._vertices = this._extrusionManager.ExtrudeByPath(profile, curve);
        Debug.Log("finished");
        //this._meshDisplayer.Display(this._vertices.ToArray(), profile.Length);
    }
}
