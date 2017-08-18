using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class Canvas2D : MonoBehaviour {

    public static Canvas Instance { get; private set; }
    public static Transform Static { get; private set; }

    [SerializeField]
    private Transform _static;

	private void Awake()
    {
        Static = _static;
        Instance = GetComponent<Canvas>();
    }
}
