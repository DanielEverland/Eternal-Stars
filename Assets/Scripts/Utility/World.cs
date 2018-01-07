using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class World {

    public static GameObject WorldObject;
    public static GameObject TerrainObject;
    
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
        
    }    
}
