using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProjectileWeapon : WeaponBase
{
    public override string AmmoCountText { get { return string.Format("{0}/{1}", MaxAmmo, CurrentAmmo); } }

    private int MaxAmmo = 100;
    private int CurrentAmmo;

    [MenuItem("Assets/Create/Items/Weapons/Projectile Weapon", priority = Utility.CREATE_ASSET_ORDER_ID)]
    private static void CreateAssetImplant()
    {
        Utility.CreateItemAndRename<ProjectileWeapon>();
    }
}
