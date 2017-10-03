using UnityEngine.AI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WeaponManager {

    public static event Action<WeaponBase> OnSelectedWeaponChanged;
    public static event Action<ItemStack> OnSelectedWeaponStackChanged;

    public static WeaponBase SelectedWeapon
    {
        get
        {
            if (_selectedStack == null)
                return null;

            return _selectedStack.Item as WeaponBase;
        }
    }
    public static ItemStack SelectedStack
    {
        get
        {
            return _selectedStack;
        }
    }

    private static void WeaponChanged()
    {
        if (OnSelectedWeaponChanged != null)
            OnSelectedWeaponChanged.Invoke(SelectedWeapon);

        if (OnSelectedWeaponStackChanged != null)
            OnSelectedWeaponStackChanged.Invoke(_selectedStack);
    }

    private static ItemStack _selectedStack;

    public static void SetWeapon(ItemStack stack)
    {
        if(stack == null)
        {
            _selectedStack = null;

            WeaponChanged();
        }
        else if(stack.Item is WeaponBase)
        {
            ItemStack oldStack = _selectedStack;

            _selectedStack = stack;

            if(oldStack != _selectedStack)
            {
                WeaponChanged();
            }
        }
        else
        {
            throw new System.ArgumentException("Tried to assign a non-weapon as currently equipped weapon");
        }
    }
}
