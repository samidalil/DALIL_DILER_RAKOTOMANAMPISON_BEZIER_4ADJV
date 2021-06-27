using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshDisplayer : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshFilter = null;

    public void Display(Vector3[] vertices,Vector3[] normales, int profileLength)
    {
        Mesh mesh = new Mesh();

        this._meshFilter.mesh = mesh;

        int bezierLength = vertices.Length / profileLength;

        List<int> indices = new List<int>();

        for (int i = 0; i < profileLength - 1; i++)
        {
            for (int j = 0; j < bezierLength - 1; j++)
            {
                // Front face
                if (i + j != 0) indices.Add((i + j * profileLength) - 1);
                else
                {
                    indices.Add(i + j * profileLength);
                    Debug.Log("is equal to 0");
                }
                indices.Add((i + (j + 1) * profileLength)-1);
                indices.Add(((i + 1) + (j + 1) * profileLength)-1);
                indices.Add(((i + 1) + j * profileLength)-1);

                // Back face
                if (i + j != 0) indices.Add((i + j * profileLength) - 1);
                else
                {
                    Debug.Log("is equal to 0");
                    indices.Add(i + j * profileLength);
                }
                indices.Add(((i + 1) + j * profileLength)-1);
                indices.Add(((i + 1) + (j + 1) * profileLength)-1);
                indices.Add((i + (j + 1) * profileLength)-1);
            }
        }

        mesh.SetVertices(vertices);
        mesh.SetNormals(normales);
        mesh.SetIndices(indices.ToArray(), MeshTopology.Quads, 0);
    }
}