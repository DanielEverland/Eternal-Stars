using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour {

    [SerializeField]
    protected float Power = 5;
    [SerializeField]
    protected float Frequency = 5;

	private int UniqueID { get; set; }

    private Vector2 offset;

    protected virtual void Awake()
    {
        UniqueID = GetInstanceID();
    }
    protected virtual void Update()
    {
        ResetShake();

        AddShake();
    }
    protected void ResetShake()
    {
        transform.position -= (Vector3)offset;

        offset = Vector2.zero;
    }
    protected void AddShake()
    {
        offset = new Vector2()
        {
            x = Mathf.PerlinNoise(UniqueID + Time.unscaledTime * Frequency, 0),
            y = Mathf.PerlinNoise(0, -UniqueID + Time.unscaledTime * Frequency),
        };

        //Shift scale from 0:1 to -1:1
        offset.x = -1 + offset.x * 2;
        offset.y = -1 + offset.y * 2;

        offset *= Power;
    }
    private void LateUpdate()
    {
        transform.position += (Vector3)offset;
    }
}
