using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Creature))]
public class PlayerItemPickupManager : MonoBehaviour {

    [SerializeField]
    private Creature player;
    [SerializeField]
    private float minDistance = 40;

    private static ItemHandler target;
    private static ContainerBase container { get { return Player.Instance.Container; } }
    
    private void Update()
    {
        CheckPlayerInput();

        if (target != null)
            AttemptToPickup();
    }
    private void CheckPlayerInput()
    {
        if (Input.anyKey)
            target = null;
    }
    private void AttemptToPickup()
    {
        Vector3 delta = target.transform.position - player.transform.position;
        delta.y = 0;

        if(delta.magnitude > minDistance)
        {
            MoveTowardsTarget();
        }
        else
        {
            DoPickup();
        }
    }
    private void MoveTowardsTarget()
    {
        TimeManager.Tick();

        player.MoveToPosition(target.transform.position);
    }
    private void DoPickup()
    {
        TimeManager.Tick();

        if (container.Fits(target.Item))
        {
          container.Add(target.Item);
        }

        PlayModeObjectPool.Pool.ReturnObject(target.gameObject);
        target = null;
    }
    public static void PickUpItem(ItemHandler itemHandler)
    {
        target = itemHandler;
    }
}
