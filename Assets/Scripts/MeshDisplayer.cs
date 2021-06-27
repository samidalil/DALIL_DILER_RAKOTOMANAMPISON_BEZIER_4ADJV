using System.Collections.Generic;
using UnityEngine;

public class MeshDisplayer : MonoBehaviour
{
    #region Variables Unity

    [SerializeField] private MeshFilter _meshFilter = null;

    #endregion

    #region Fonctions Unity

    private void Awake()
    {
        this._meshFilter.mesh = new Mesh();
    }

    #endregion

    #region Fonctions publiques

    public void Display(Vector3[] vertices, int profileLength)
    {
        Mesh mesh = this._meshFilter.mesh;

        mesh.Clear();

        int bezierLength = vertices.Length / profileLength;

        List<int> indices = new List<int>();

        for (int i = 0; i < profileLength - 1; i++)
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

        mesh.SetVertices(vertices);
        mesh.SetIndices(indices.ToArray(), MeshTopology.Quads, 0);
    }

    #endregion
}