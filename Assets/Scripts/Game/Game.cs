using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game {

    public static Game Instance { get; private set; }
    public static void Initialize()
    {
        if (Instance == null)
        {
            Instance = new Game();
        }
    }

    public static Map CurrentMap { get { return Map.CurrentMap; } }

    private Game()
    {
        MapGenerator.GenerateMap();
    }
}
