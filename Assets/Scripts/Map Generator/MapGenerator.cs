using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class MapGenerator {

    private static Map _currentMap;

    public static UnityEvent<Map> OnMapGenerated { get; set; }
    public static UnityEvent OnDestroyMap { get; set; }

    public static void GenerateMap()
    {
        if(_currentMap != null)
        {
            if (OnDestroyMap != null)
                OnDestroyMap.Invoke();
        }
        
        _currentMap = new Map();

        if(OnMapGenerated != null)
        {
            OnMapGenerated.Invoke(_currentMap);
        }
    }
}
