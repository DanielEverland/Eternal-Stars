using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : AIBehaviour {

    protected new virtual EnemyData Data { get { return (EnemyData)base.Data; } }

    protected override void Awake()
    {
        base.Awake();

        Brain.Initialize(this);
    }
    protected override void Die()
    {
        base.Die();

        PollDropLoot();
    }
    protected virtual void PollDropLoot()
    {
        if (Data.DropChance > 0 && Data.AmountOfDropableItems > 0)
        {
            float poll = Random.Range(0f, 1f);

            if (poll < Data.DropChance)
            {
                DropItem();
            }
        }
    }
    protected virtual void DropItem()
    {
        ItemBase itemToDrop = Data.GetDropableItem();

        ItemHandler.DropItem(itemToDrop, transform.position);
    }
}
