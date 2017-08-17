using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    [SerializeField]
    private Creature creature;

    private Vector3 direction;

    private void Update()
    {
        PollDirection();
        Move();
    }
    private void PollDirection()
    {
        direction = Vector2.zero;

        if(Keybindings.GetKey("Move Right"))
        {
            direction += Vector3.right;
        }
        if (Keybindings.GetKey("Move Left"))
        {
            direction += Vector3.left;
        }
        if (Keybindings.GetKey("Move Up"))
        {
            direction += Vector3.forward;
        }
        if (Keybindings.GetKey("Move Down"))
        {
            direction += Vector3.back;
        }
    }
    private void Move()
    {
        creature.MoveInDirection(direction.normalized);

        if(direction != Vector3.zero)
        {
            TimeManager.Tick();
        }
    }
}
