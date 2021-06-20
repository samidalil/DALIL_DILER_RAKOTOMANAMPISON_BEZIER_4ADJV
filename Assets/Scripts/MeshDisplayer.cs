using System.Collections.Generic;
using UnityEngine;

public class MeshDisplayer : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshFilter = null;

    public void Display(Vector3[] vertices, int length)
    {
        Mesh mesh = new Mesh();

        this._meshFilter.mesh = mesh;

        List<int> indices = new List<int>();

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                // front face
                indices.Add(i + j * length);
                indices.Add(i + (j+1) * length);
                indices.Add((i+1) + (j+1) * length);
                indices.Add((i+1) + j * length);

                // back face
                indices.Add((i + 1) + j * length);
                indices.Add((i + 1) + (j + 1) * length);
                indices.Add(i + (j + 1) * length);
                indices.Add(i + j * length);
            }
        }

        mesh.SetVertices(vertices);
        mesh.SetIndices(indices.ToArray(), MeshTopology.Quads, 0);
    }
}