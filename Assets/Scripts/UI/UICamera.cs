using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UICamera : MonoBehaviour {

    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private new Camera camera;

	private void Update()
    {
        camera.orthographicSize = ((RectTransform)canvas.transform).rect.height / 2;
    }
}
