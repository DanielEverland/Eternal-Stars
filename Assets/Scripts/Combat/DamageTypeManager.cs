using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageTypeManager.asset", menuName = "Combat/Damage Type Manager", order = Utility.CREATE_ASSET_ORDER_ID)]
public class DamageTypeManager : ScriptableObject {

    public static DamageTypes EnemyCritical { get { return Instance._enemyCritical; } }
    [SerializeField] private DamageTypes _enemyCritical;

    public static DamageTypes EnemyNormal { get { return Instance._enemyNormal; } }
    [SerializeField] private DamageTypes _enemyNormal;

    public static DamageTypes PlayerCritical { get { return Instance._playerCritical; } }
    [SerializeField] private DamageTypes _playerCritical;

    public static DamageTypes PlayerNormal { get { return Instance._playerNormal; } }
    [SerializeField] private DamageTypes _playerNormal;

    public static DamageTypes HealNormal { get { return Instance._healNormal; } }
    [SerializeField] private DamageTypes _healNormal;

    public static DamageTypes HealCritical { get { return Instance._healCritical; } }
    [SerializeField] private DamageTypes _healCritical;

    private static DamageTypeManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = (DamageTypeManager)Resources.Load("DamageTypeManager");
            }

            return _instance;
        }
    }
    private static DamageTypeManager _instance;
}
