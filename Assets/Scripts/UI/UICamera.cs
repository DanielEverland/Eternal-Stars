using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UICamera : MonoBehaviour {
    
    [SerializeField]
    private new Camera camera;

	private void Update()
    {
        camera.orthographicSize = Screen.height / 2;
        camera.transform.position = new Vector3()
        {
            x = Screen.width / 2,
            y = Screen.height / 2,
            z = -1,
        };
    }
}
