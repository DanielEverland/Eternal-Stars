using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Creature))]
public class CreatureQuadtreeReporter : MonoBehaviour {

    [SerializeField]
    private Creature creature;

    private const float MIN_DISTANCE = 5;
    
	private void Update()
    {
        CreatureManager.Add(creature);
    }
    private void OnValidate()
    {
        creature = GetComponent<Creature>();
    }
}
