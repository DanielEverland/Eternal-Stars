using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

    public static bool HasLoaded { get; private set; }

    private List<Action> OnGameStart = new List<Action>()
    {
        { Keybindings.Load },
        { PlayModeManager.LoadGlobalManagers },
        { PlayModeManager.LoadPlayer },
    };

	private void Awake()
    {
        OnGameStart.ForEach(x => x.Invoke());

        HasLoaded = true;

        Destroy(this);
    }
}
