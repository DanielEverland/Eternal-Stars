using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMover : MonoBehaviour {

    [SerializeField]
    private Vector2 Direction;
    [SerializeField]
    private float Speed = 10;

    private void Update()
    {
        transform.position += (Vector3)Direction.normalized * (Speed * Time.unscaledDeltaTime);
    }
}
