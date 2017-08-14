using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : MonoBehaviour {

    [SerializeField]
    private List<PrefabEntry> prefabs;
    
    private static DamageTextManager instance;
    private static Dictionary<DamageTypes, string> poolKeyLookup;

    private const float OFFSET_RANGE = 50;

    private void Awake()
    {
        instance = this;

        CreatePoolKeyLookup();
    }
    private void CreatePoolKeyLookup()
    {
        poolKeyLookup = new Dictionary<DamageTypes, string>();

        for (int i = 0; i < prefabs.Count; i++)
        {
            PrefabEntry entry = prefabs[i];

            poolKeyLookup.Add(entry.damageType, entry.objectPoolKey);
        }
    }
	public static void AddDamage(float damageAmount, DamageTypes damageType, GameObject obj)
    {
        string objectPoolKey = poolKeyLookup[damageType];

        DamageTextElement element = PlayModeObjectPool.Pool.GetObject(objectPoolKey).GetComponent<DamageTextElement>();

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
    [Serializable]
    private struct PrefabEntry
    {
        public DamageTypes damageType;
        public string objectPoolKey;
    }
}
