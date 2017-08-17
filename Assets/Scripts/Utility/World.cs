using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    public static GameObject WorldObject;

    private void Awake()
    {
        WorldObject = gameObject;
    }
    public static void AddToWorld(GameObject obj)
    {
        obj.transform.SetParent(WorldObject.transform, false);
    }
}
