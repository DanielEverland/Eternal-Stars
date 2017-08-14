using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowScript : MonoBehaviour {

    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float distance;
    [SerializeField]
    private float angle;
    [SerializeField]
    private float lerpSpeed;

    private Vector3 targetPosition;
    
    private void Update()
    {
        PollTransform();
        LerpValues();
    }
    private void PollTransform()
    {
        Vector3 direction = Vector3.back;
        direction = Quaternion.Euler(angle, 0, 0) * direction;

        targetPosition = target.transform.position + direction * distance;
        transform.eulerAngles = new Vector3(angle, 0, 0);
    }
    private void LerpValues()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.unscaledDeltaTime);
    }
    private void OnValidate()
    {
        PollTransform();
    }
}
