using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Polygon))]
public class PolygonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        this.DrawDefaultInspector();

        if (GUILayout.Button("Add child points"))
            this.AddChildPoints((Polygon) target);
    }

    private void AddChildPoints(Polygon target)
    {
        target.Points = new List<Point>(target.GetComponentsInChildren<Point>());
    }
}
