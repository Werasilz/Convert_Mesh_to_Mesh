using UnityEngine;

public class ConvertMesh : MonoBehaviour
{
    public Mesh source;
    public GameObject output;

    private void Start()
    {
        output.GetComponent<MeshFilter>().mesh = Convert(source, output.GetComponent<MeshFilter>().mesh);
    }

    Mesh Convert(Mesh mainMesh, Mesh outputMesh)
    {
        if (mainMesh.vertices.Length > outputMesh.vertices.Length) return null;
        if (mainMesh.uv.Length > outputMesh.uv.Length) return null;
        if (mainMesh.GetIndices(0).Length > outputMesh.GetIndices(0).Length) return null;

        Vector3[] verticesArray = new Vector3[outputMesh.vertices.Length];
        Vector2[] uvsArray = new Vector2[outputMesh.uv.Length];
        int[] indicesArray = new int[outputMesh.GetIndices(0).Length];

        var vertices = mainMesh.vertices;
        var uvs = mainMesh.uv;
        var indices = mainMesh.GetIndices(0);

        int j = 0;
        for (int i = 0; i < verticesArray.Length; i++)
        {
            verticesArray[i] = new Vector3(vertices[j].x, vertices[j].y, vertices[j].z);
            j++;
            if (j > mainMesh.vertices.Length - 1)
                j = 0;
        }

        int n = 0;
        for (int i = 0; i < uvsArray.Length; i++)
        {
            uvsArray[i] = new Vector2(uvs[n].x, uvs[n].y);
            n++;
            if (n > mainMesh.uv.Length - 1)
                n = 0;
        }

        int k = 0;
        for (int i = 0; i < indicesArray.Length; i++)
        {
            indicesArray[i] = indices[k];
            k++;
            if (k > mainMesh.triangles.Length - 1)
                k = 0;
        }

        outputMesh.vertices = verticesArray;
        outputMesh.uv = uvsArray;
        outputMesh.SetIndices(indicesArray, MeshTopology.Triangles, 0);

        return outputMesh;
    }
}
