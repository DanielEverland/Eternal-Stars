using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class World {

    public static GameObject WorldObject;
    public static GameObject TerrainObject;

    public static int TILE_SIZE = 32;
    
    public static void Initialize()
    {
        WorldObject = new GameObject("World");
        TerrainObject = new GameObject("Terrain");

        CreateTerrain();
    }

    public static void AddToWorld(GameObject obj)
    {
        obj.transform.SetParent(WorldObject.transform, false);
    }
    private static void CreateTerrain()
    {
        Mesh mesh = MeshGenerator.GetGridMesh(new List<IntVector2>(Game.CurrentMap.TilePositions.Select(x => (IntVector2)x)));

        MeshFilter filter = TerrainObject.AddComponent<MeshFilter>();
        TerrainObject.AddComponent<MeshRenderer>();

        filter.mesh = mesh;
    }    
}
