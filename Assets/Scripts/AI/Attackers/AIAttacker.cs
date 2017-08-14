using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAttacker : AIComponent {

    public abstract void PollAttack();
    protected abstract void DoAttack();
}
