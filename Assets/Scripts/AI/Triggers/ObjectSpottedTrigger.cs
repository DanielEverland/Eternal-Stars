using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectSpottedTrigger.asset", menuName = "AI/Triggers/Object Spotter", order = Utility.CREATE_ASSET_ORDER_ID)]
public class ObjectSpottedTrigger : AITrigger {

    [SerializeField]
    private AIObjectRecognizer ObjectRecognizer;
    [SerializeField]
    private float SpotRadius;

    public override void Initialize(AIState stateOwner, EnemyBase owner, AIBrain brain)
    {
        base.Initialize(stateOwner, owner, brain);

        Brain.Target = null;
    }
    public override bool Evaluate()
    {
        List<Creature> creaturesWithinRange = CreatureManager.GetCreatures(Owner.transform.position, SpotRadius);

        for (int i = 0; i < creaturesWithinRange.Count; i++)
        {
            if (ObjectRecognizer.Poll(creaturesWithinRange[i].gameObject))
            {
                Brain.Target = creaturesWithinRange[i].gameObject;
                return true;
            }
        }

        return false;
    }
}
