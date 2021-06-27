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

    public void Display(Vector3[] vertices, Vector3[] normales, int profileLength)
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

    #endregion
}