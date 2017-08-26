using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour {
    
    private static DamageHandler instance;

    private void Awake()
    {
        instance = this;
    }    
    public static void DealDamage(GameObject toObject, float amount)
    {
        DamageTaker damageTaker = toObject.GetComponent<DamageTaker>();
        TargetType targetType = toObject.GetComponent<PlayerIdentifier>() == null ? TargetType.Enemy : TargetType.Player;

        if (damageTaker == null)
            Debug.LogError("Object " + toObject + " cannot take damage without a damage handler");

        damageTaker.TakeDamage(amount);

        float poll = UnityEngine.Random.Range(0f, 100f);
        DamageTypes type = GetNormalType(targetType);

        if (poll > 80)
            type = GetCriticalType(targetType);

        DamageTextManager.AddDamage(amount, type, toObject);
    }
    private static DamageTypes GetNormalType(TargetType type)
    {
        switch (type)
        {
            case TargetType.Player:
                return DamageTypeManager.PlayerNormal;
            case TargetType.Enemy:
                return DamageTypeManager.EnemyNormal;
            default:
                throw new ArgumentException();
        }
    }
    private static DamageTypes GetCriticalType(TargetType type)
    {
        switch (type)
        {
            case TargetType.Player:
                return DamageTypeManager.PlayerCritical;
            case TargetType.Enemy:
                return DamageTypeManager.EnemyCritical;
            default:
                throw new ArgumentException();
        }
    }
    private enum TargetType
    {
        Player,
        Enemy,
    }
}
