using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemAction : ScriptableObject {

    public abstract string Description { get; }
    public abstract void DoAction(ItemStack caller);
}
