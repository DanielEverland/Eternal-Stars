using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour {

    [SerializeField]
    private float FiringInterval = 0.2f;

    private float lastFiredTime;
    
    private void Awake()
    {
        lastFiredTime = -FiringInterval;
    }
	private void Update()
    {
        if (Keybindings.GetKey("Shoot"))
        {
            TimeManager.Tick();

            PollCanFire();
        }
    }
    private void PollCanFire()
    {
        if(Time.time - lastFiredTime > FiringInterval)
        {
            Fire();
        }
    }
    private void Fire()
    {
        lastFiredTime = Time.time;

        CreateProjectile();
    }
    private void CreateProjectile()
    {
        Projectile projectile = PlayModeObjectPool.Pool.GetObject("PlayerProjectile").GetComponent<Projectile>();
        projectile.InitializeMouse(transform.position);
    }
}
