using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSidedQuadCreator {

    [MenuItem("Assets/Create/Mesh/Double Sided Quad", priority = Utility.CREATE_ASSET_ORDER_ID)]
    private static void Create()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[8]
        {
            new Vector3(-0.5f, 0.5f), 
            new Vector3(0.5f, 0.5f), 
            new Vector3(0.5f, -0.5f), 
            new Vector3(-0.5f, -0.5f),

            new Vector3(-0.5f, 0.5f),
            new Vector3(0.5f, 0.5f),
            new Vector3(0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f),
        };
        
        Vector2[] uvs = new Vector2[8]
        {
            new Vector2(1, 1),
            new Vector2(0, 1),
            new Vector2(0, 0),
            new Vector2(1, 0),

            new Vector2(1, 1),
            new Vector2(0, 1),
            new Vector2(0, 0),
            new Vector2(1, 0),
        };

        int[] triangles = new int[12]
        {
            0, 1, 2,
            2, 3, 0,
            6, 5, 4,
            4, 7, 6,
        };

        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        AssetDatabase.CreateAsset(mesh, AssetDatabase.GetAssetPath(Selection.activeObject) + "/DoubleSidedQuad.asset");
    }
}
