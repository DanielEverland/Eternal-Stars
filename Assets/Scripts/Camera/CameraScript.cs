using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    [SerializeField]
    private new Camera camera;

	private void Awake()
    {
        SetCameraSettings();
    }
    private void OnValidate()
    {
        SetCameraSettings();
    }
    private void SetCameraSettings()
    {
        camera.orthographic = true;
        camera.orthographicSize = Screen.height / 2;
    }
}
