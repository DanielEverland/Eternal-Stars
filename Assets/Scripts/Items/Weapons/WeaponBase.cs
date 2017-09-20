using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponBase : EquipableItem
{
    public override EquipmentTypes EquipmentType
    {
        get
        {
            return EquipmentTypes.Weapon;
        }
    }

    public Sprite WeaponAppearance { get { return _weaponAppearance; } }

    [SerializeField]
    private Sprite _weaponAppearance;
    [SerializeField]
    private bool _doubleHanded;

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Items/Weapon", priority = Utility.CREATE_ASSET_ORDER_ID)]
    private static void CreateAssetImplant()
    {
        Utility.CreateItemAndRename<WeaponBase>();
    }
#endif
}
