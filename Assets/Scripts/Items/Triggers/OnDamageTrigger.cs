using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDamageTrigger : ItemTrigger
{
    public override void OnSubscribe()
    {
        Player.Instance.OnTakeDamage += Trigger;
    }
    public override void OnUnsubscribe()
    {
        Player.Instance.OnTakeDamage -= Trigger;
    }
}
