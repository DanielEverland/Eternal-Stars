using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class World {

    public static GameObject WorldObject;
    public static GameObject TerrainObject;

    private static ChunkCollection<TileType> _terrainChunkCollection;

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
        _terrainChunkCollection = new ChunkCollection<TileType>();

        foreach (Vector2 tilePosition in Map.CurrentMap.TilePositions)
        {
            int typeIndex = Random.Range(0, TileType.AllTypes.Count - 1);
            TileType tileType = TileType.AllTypes[typeIndex];

            _terrainChunkCollection.Add((IntVector2)tilePosition, tileType);
        }
    }    
}
