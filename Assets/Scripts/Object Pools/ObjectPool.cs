using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectPool.asset", menuName = "Utility/Object Pool", order = Utility.CREATE_ASSET_ORDER_ID)]
public class ObjectPool : ScriptableObject {

    public IEnumerable<PoolEntry> Entries { get { return _entries; } }

    [SerializeField]
    private List<PoolEntry> _entries;
    
    [System.Serializable]
    public struct PoolEntry
    {
        public GameObject Prefab;
        public string Key;
        public int Amount;
    }
}
