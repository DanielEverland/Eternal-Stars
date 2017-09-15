using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemTrigger : ScriptableObject {

    public event Action OnTrigger;

    public abstract void OnSubscribe();
    public abstract void OnUnsubscribe();

    protected void Trigger()
    {
        if (OnTrigger != null)
            OnTrigger.Invoke();
    }
}
