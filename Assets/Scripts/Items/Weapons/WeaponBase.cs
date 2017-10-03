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

    public override void OnEquipped(ItemStack stack)
    {
        base.OnEquipped(stack);

        if (WeaponManager.SelectedStack == null)
            WeaponManager.SetWeapon(stack);
    }
    public override void OnUnequipped(ItemStack stack)
    {
        base.OnUnequipped(stack);

        if (WeaponManager.SelectedStack == stack)
            WeaponManager.SetWeapon(null);
    }

    [SerializeField]
    private Sprite _weaponAppearance;
    [SerializeField]
    private bool _doubleHanded;
}
