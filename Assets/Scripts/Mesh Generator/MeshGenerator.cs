using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator {
    
	public static Mesh GetGridMesh(List<IntVector2> cellPositions)
    {
        Vector3[] vertices = new Vector3[cellPositions.Count * 4];
        int[] triangles = new int[cellPositions.Count * 6];
        int vertexCount = 0;
        int triangleCount = 0;

        foreach (IntVector2 position in cellPositions)
        {
            AddVertices(position, vertexCount, vertices);
            AddTriangles(triangleCount, vertexCount, triangles);

            vertexCount += 4;
            triangleCount += 6;
        }

        Mesh mesh = new Mesh();

        mesh.name = string.Format("Procedural Mesh ({0})", Time.frameCount);
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        return mesh;
    }
    private static void AddVertices(IntVector2 position, int index, Vector3[] container)
    {
        container[0 + index] = new Vector3(-0.5f + position.x, 0, 0.5f + position.y) * World.TILE_SIZE;
        container[1 + index] = new Vector3(0.5f + position.x, 0, 0.5f + position.y) * World.TILE_SIZE;
        container[2 + index] = new Vector3(0.5f + position.x, 0, -0.5f + position.y) * World.TILE_SIZE;
        container[3 + index] = new Vector3(-0.5f + position.x, 0, -0.5f + position.y) * World.TILE_SIZE;
    }
    private static void AddTriangles(int index, int vertexCount, int[] triangles)
    {
        triangles[0 + index] = vertexCount + 0;
        triangles[1 + index] = vertexCount + 1;
        triangles[2 + index] = vertexCount + 2;

        triangles[3 + index] = vertexCount + 2;
        triangles[4 + index] = vertexCount + 3;
        triangles[5 + index] = vertexCount + 0;
    }
}
