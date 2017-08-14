using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : Creature {

    [SerializeField]
    private AIBrain _brainTemplate;

    private float timeSinceLastThought;
    protected AIBrain Brain { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        if (_brainTemplate == null)
            Debug.LogError("Missing brain", gameObject);

        Brain = Instantiate(_brainTemplate);
    }
    private void Update()
    {
        timeSinceLastThought += Time.deltaTime;

        if(timeSinceLastThought > Brain.ThinkRate)
        {
            Brain.Think();
        }

        Brain.Update();
    }
}
