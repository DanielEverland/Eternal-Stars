using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

    private List<Action> OnGameStart = new List<Action>()
    {
        { Keybindings.Load },
    };

	private void Awake()
    {
        OnGameStart.ForEach(x => x.Invoke());

        Destroy(gameObject);
    }
}
