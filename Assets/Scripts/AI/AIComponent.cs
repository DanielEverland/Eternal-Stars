using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIComponent : ScriptableObject {

	public AIBrain Brain { get; private set; }
    public EnemyBase Owner { get; private set; }
    public GameObject GameObject { get { return Owner.gameObject; } }
    public Transform Transform { get { return GameObject.transform; } }
    
    public virtual void Initialize(EnemyBase owner, AIBrain brain)
    {
        Owner = owner;
        Brain = brain;
    }
}
