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
    
    private void Update()
    {
        if (target != null)
            AttemptToPickup();
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
        player.MoveToPosition(target.transform.position);
    }
    private void DoPickup()
    {
        PlayModeObjectPool.Pool.ReturnObject(target.gameObject);
    }
    public static void PickUpItem(ItemHandler itemHandler)
    {
        target = itemHandler;
    }
}
