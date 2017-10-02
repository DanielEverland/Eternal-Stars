using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WeaponManager {

    public static event Action<WeaponBase> OnSelectedWeaponChanged;

	public static WeaponBase SelectedWeapon
    {
        get
        {
            if(_selectedWeapon == null && Player.Instance.EquipmentContainer.ContainsType(EquipmentTypes.Weapon))
            {
                _selectedWeapon = (WeaponBase)Player.Instance.EquipmentContainer.GetItem(EquipmentTypes.Weapon);

                WeaponChanged();
            }

            return _selectedWeapon;
        }
        set
        {
            WeaponBase oldWeapon = _selectedWeapon;

            _selectedWeapon = value;

            if(oldWeapon != _selectedWeapon)
            {
                WeaponChanged();
            }
        }
    }
    private static void WeaponChanged()
    {
        if (OnSelectedWeaponChanged != null)
            OnSelectedWeaponChanged.Invoke(_selectedWeapon);
    }

    private static WeaponBase _selectedWeapon;
}
