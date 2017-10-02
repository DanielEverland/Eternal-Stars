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

    public override void OnEquipped()
    {
        base.OnEquipped();

        if (WeaponManager.SelectedWeapon == null)
            WeaponManager.SelectedWeapon = this;
    }
    public override void OnUnequipped()
    {
        base.OnUnequipped();

        if (WeaponManager.SelectedWeapon == this)
            WeaponManager.SelectedWeapon = null;
    }

    public Sprite WeaponAppearance { get { return _weaponAppearance; } }

    [SerializeField]
    private Sprite _weaponAppearance;
    [SerializeField]
    private bool _doubleHanded;
}
