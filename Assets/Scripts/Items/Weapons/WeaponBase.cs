using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class WeaponBase : EquipableItem
{
    public override EquipmentTypes EquipmentType
    {
        get
        {
            return EquipmentTypes.Weapon;
        }
    }

    /// <summary>
    /// This will be displayed in the currently selected ammo text
    /// </summary>
    public abstract string AmmoCountText { get; }
    
    public Sprite WeaponAppearance { get { return _weaponAppearance; } }

    protected override void OnEquipped(ItemStack stack)
    {
        if (WeaponManager.SelectedStack == null)
            WeaponManager.SetWeapon(stack);
    }
    protected override void OnUnequipped(ItemStack stack)
    {
        if (WeaponManager.SelectedStack == stack)
            WeaponManager.SetWeapon(null);
    }

    [SerializeField]
    private Sprite _weaponAppearance;
    [SerializeField]
    private bool _doubleHanded;
}
