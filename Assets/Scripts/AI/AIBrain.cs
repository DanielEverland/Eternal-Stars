using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Behaviour.asset", menuName = "AI/AI Behaviour", order = Utility.CREATE_ASSET_ORDER_ID)]
public class AIBrain : ScriptableObject {

    [Tooltip("Times to think per second")]
    [SerializeField]
    private float _thinkRate = 10;
    
    [SerializeField]
    private AIState _startState;
    [SerializeField]
    private AIAttacker _attacker;
    
    /// <summary>
    /// Time in seconds between thought
    /// </summary>
    public float ThinkRate { get { return 1 / _thinkRate; } }
    public AIAttacker Attacker { get; private set; }
    public AIState CurrentState { get; private set; }
    public EnemyBase Owner { get; private set; }

    public GameObject Target { get { return (GameObject)GetData("Target"); } set { AssignData("Target", value); } }

    private Dictionary<string, object> _runtimeData;
    
    public void Initialize(EnemyBase owner)
    {
        Owner = owner;
        _runtimeData = new Dictionary<string, object>();

        if (_attacker != null)
        {
            Attacker = Instantiate(_attacker);
            Attacker.Initialize(owner, this);
        }
        else
        {
            Debug.LogWarning("No attacker has been assigned to this brain", this);
        }

        if(_startState != null)
        {
            CurrentState = Instantiate(_startState);
            CurrentState.Initialize(owner, this);
        }
        else
        {
            Debug.LogError("No start state is assigned to this brain", this);
        }
    }
    public void AssignData(string key, object data)
    {
        if (_runtimeData.ContainsKey(key))
        {
            _runtimeData[key] = data;
        }
        else
        {
            _runtimeData.Add(key, data);
        }
    }
    public void RemoveData(string key)
    {
        if (_runtimeData.ContainsKey(key))
        {
            _runtimeData.Remove(key);
        }
    }
    public bool ContainsData(string key)
    {
        return _runtimeData.ContainsKey(key);
    }
    public object GetData(string key)
    {
        if (ContainsData(key))
        {
            return _runtimeData[key];
        }
        else
        {
            return null;
        }
    }
    public void ChangeState(AIState state)
    {
        CurrentState = Instantiate(state);
        CurrentState.Initialize(Owner, this);
    }
    public void Think()
    {
        CurrentState.Think();
    }
    public void Update()
    {
        CurrentState.Update();
    }
}
