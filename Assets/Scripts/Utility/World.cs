using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World {

    public static GameObject WorldObject;
    
    public static void AddToWorld(GameObject obj)
    {
        obj.transform.SetParent(WorldObject.transform, false);
    }
    public static void Initialize()
    {
        WorldObject = new GameObject("World");
    }
}
