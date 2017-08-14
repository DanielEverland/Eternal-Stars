using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageType.asset", menuName = "Combat/Damage Type", order = Utility.CREATE_ASSET_ORDER_ID)]
public class DamageTypes : ScriptableObject {

    public string Name { get { return _name; } }

    [SerializeField]
    private string _name;
}
