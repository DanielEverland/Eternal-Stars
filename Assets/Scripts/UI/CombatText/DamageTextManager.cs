using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : MonoBehaviour {
        
    private static DamageTextManager instance;
    private static Dictionary<DamageTypes, string> poolKeyLookup;

    private const float OFFSET_RANGE = 50;
    
	public static void AddDamage(float damageAmount, DamageTypes damageType, GameObject obj)
    {
        DamageTextElement element = PlayModeObjectPool.Pool.GetObject(damageType.Name).GetComponent<DamageTextElement>();

        element.UpdateText(damageAmount, obj.transform.position + GetRandomOffset());
        element.transform.SetParent(Canvas2D.Instance.transform);
    }
    private static Vector3 GetRandomOffset()
    {
        return new Vector3()
        {
            x = UnityEngine.Random.Range(-OFFSET_RANGE, OFFSET_RANGE),
            z = UnityEngine.Random.Range(0, OFFSET_RANGE),
        };
    }
}
