using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WeaponManager {

	public static WeaponBase SelectedWeapon
    {
        get
        {
            return _selectedWeapon;
        }
        set
        {
            _selectedWeapon = value;
        }
    }
    private static WeaponBase _selectedWeapon;
}
