using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreatureData.asset", menuName = "Creature/Create Base Data", order = Utility.CREATE_ASSET_ORDER_ID)]
public class CreatureData : ScriptableObject {
    
    public int Health { get { return _health; } }
    public int Speed { get { return _speed; } }
    public Vector2 BoundsCenter { get { return _boundsCenter; } }
    public Vector2 BoundsSize { get { return _boundsSize; } }

    [Header("Base Properties")]
    [Range(0, 1000)]
    [SerializeField]
    private int _health = 100;
    [Range(0, 1000)]
    [SerializeField]
    private int _speed = 100;

    [Space()]

    [Header("Advanced")]
    [SerializeField]
    private Vector2 _boundsCenter;
    [SerializeField]
    private Vector2 _boundsSize = Vector2.one;

    public virtual void AssignData(Creature creature)
    {

    }
}