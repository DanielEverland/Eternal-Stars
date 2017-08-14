using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState : AIComponent
{
    public CharacterController Controller { get { return _controller; } }

    [SerializeField] private AITrigger NextStateTrigger;
    [SerializeField] private AIState NextState;
    [SerializeField] private bool CanAttack;
    
    private CharacterController _controller;

    public override void Initialize(EnemyBase owner, AIBrain brain)
    {
        base.Initialize(owner, brain);
        
        _controller = Owner.GetComponent<CharacterController>();
        
        if(NextStateTrigger == null)
        {
            Debug.LogWarning("No state trigger has been assigned", this);
        }
        else
        {
            NextStateTrigger = Instantiate(NextStateTrigger);
            NextStateTrigger.Initialize(this, owner, brain);
        }        
    }
    public virtual void Think()
    {
        if (NextStateTrigger == null || NextState == null)
            return;
        
        if (NextStateTrigger.Evaluate())
        {
            Brain.ChangeState(NextState);
        }
    }
	public virtual void Update()
    {
        if (CanAttack && Brain.Attacker != null)
        {
            Brain.Attacker.PollAttack();
        }
    }
}
