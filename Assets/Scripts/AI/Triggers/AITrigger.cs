using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AITrigger : AIComponent {
    
    public AIState State { get { return _state; } }

    private AIState _state;

    public virtual void Initialize(AIState stateOwner, EnemyBase owner, AIBrain brain)
    {
        base.Initialize(owner, brain);

        _state = stateOwner;
    }
    public abstract bool Evaluate();
}
