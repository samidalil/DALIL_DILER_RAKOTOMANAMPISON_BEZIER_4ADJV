using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshDisplayer : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshFilter = null;

    public void Display(Vector3[] vertices, int profileLength)
    {
        Mesh mesh = new Mesh();

        this._meshFilter.mesh = mesh;

        int bezierLength = vertices.Length / profileLength;

        Debug.Log($"Bezier: {bezierLength}, Profile: {profileLength}, Vertices: {vertices.Length}");

        List<int> indices = new List<int>();

        for (int i = 0; i < profileLength; i++)
        {
            for (int j = 0; j < bezierLength - 1; j++)
            {
                // Front face
                indices.Add(i + j * profileLength);
                indices.Add(i + (j + 1) * profileLength);
                indices.Add((i + 1) + (j + 1) * profileLength);
                indices.Add((i + 1) + j * profileLength);

                // Back face
                indices.Add(i + j * profileLength);
                indices.Add((i + 1) + j * profileLength);
                indices.Add((i + 1) + (j + 1) * profileLength);
                indices.Add(i + (j + 1) * profileLength);
            }
        }

        Debug.Log(indices.Max());


        mesh.SetVertices(vertices);
        mesh.SetIndices(indices.ToArray(), MeshTopology.Quads, 0);
    }
}