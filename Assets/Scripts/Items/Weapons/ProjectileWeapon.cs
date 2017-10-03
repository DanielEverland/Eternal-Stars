using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProjectileWeapon : WeaponBase
{
    public override string AmmoCountText { get { return string.Format("{0}/{1}", MaxAmmo, CurrentAmmo); } }

    [SerializeField]
    private float FiringInterval = 0.2f;
    [SerializeField]
    private int MaxAmmo = 100;
    
    private int CurrentAmmo { get { return CurrentStack.GetRuntimeData<int>("CurrentAmmo"); } set { CurrentStack.SetRuntimeData("CurrentAmmo", value); } }
    private float LastTimeFired { get { return CurrentStack.GetRuntimeData<float>("LastTimeFired"); } set { CurrentStack.SetRuntimeData("LastTimeFired", value); } }

    public override void OnEquipped(ItemStack stack)
    {
        base.OnEquipped(stack);

        LastTimeFired = -FiringInterval;
    }
    public override void OnUpdate(ItemStack stack)
    {
        base.OnUpdate(stack);

        if (Keybindings.GetKey("Shoot") && !EG_Input.IsSuppressed)
        {
            TimeManager.Tick();

            PollCanFire();
        }
    }
    private void PollCanFire()
    {
        if (Time.time - LastTimeFired > FiringInterval)
        {
            Fire();
        }
    }
    private void Fire()
    {
        LastTimeFired = Time.time;

        CurrentAmmo--;

        if (CurrentAmmo < 0)
            CurrentAmmo = MaxAmmo;

        CreateProjectile();
    }
    private void CreateProjectile()
    {
        Projectile projectile = PlayModeObjectPool.Pool.GetObject("PlayerProjectile").GetComponent<Projectile>();
        projectile.InitializeMouse(Player.Instance.transform.position);
    }

    [MenuItem("Assets/Create/Items/Weapons/Projectile Weapon", priority = Utility.CREATE_ASSET_ORDER_ID)]
    private static void CreateAssetImplant()
    {
        Utility.CreateItemAndRename<ProjectileWeapon>();
    }
}
