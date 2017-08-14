using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpOffset : MonoBehaviour {

    [SerializeField]
    private Vector2 Offset;
    [SerializeField]
    private float LerpSpeed = 5;

    private Vector2 targetPos;

    private void Start()
    {
        SetAnchor(transform.position);
    }
    public void SetAnchor(Vector2 anchor)
    {
        targetPos = anchor + Offset;
    }
    private void Update()
    {
        transform.position = Vector2.Lerp(transform.position, targetPos, LerpSpeed * Time.unscaledDeltaTime);
    }
}
