using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour {
    
	private void Update()
    {
        if(WeaponManager.SelectedWeapon != null)
        {
            WeaponManager.SelectedWeapon.CallUpdate(WeaponManager.SelectedStack);
        }
    }
}
