using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class Canvas2D : MonoBehaviour {

    public static Canvas Instance { get; private set; }

	private void Awake()
    {
        Instance = GetComponent<Canvas>();
    }
}
