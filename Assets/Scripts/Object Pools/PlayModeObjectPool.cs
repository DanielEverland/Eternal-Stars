using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayModeObjectPool : ObjectPoolBehaviour {

    public static ObjectPoolBehaviour Pool { get { return _pool; } }
    private static ObjectPoolBehaviour _pool;

    protected override void Awake()
    {
        base.Awake();

        _pool = this;
    }
}
