using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData.asset", menuName = "Creature/Enemy Data", order = Utility.CREATE_ASSET_ORDER_ID)]
public class EnemyData : CreatureData {
    
    public float DropChance { get { return _dropChance; } }
    public int AmountOfDropableItems { get { return _dropableItemEntries.Count; } }
    public IEnumerable<DropableItemEntry> DropableItems
    {
        get
        {
            if (_dropableItems == null)
                CreateDropableItems();

            return _dropableItems;
        }
    }

    private static List<DropableItemEntry> _dropableItems;
    
    [Space()]
    [Header("Loot")]

    [Range(0, 1)]
    [SerializeField]
    private float _dropChance = 0.5f;
    [SerializeField]
    private List<DropableItemEntry> _dropableItemEntries;
    

    public ItemBase GetDropableItem()
    {
        if (_dropableItems == null)
            CreateDropableItems();

        float poll = Random.Range(0f, 1f);
        float handicap = 0;

        for (int i = 0; i < _dropableItems.Count; i++)
        {
            if (poll <= _dropableItems[i].dropChance + handicap)
            {
                return Object.Instantiate(_dropableItems[i].item);
            }

            handicap += _dropableItems[i].dropChance;
        }

        throw new System.Exception("Couldn't find an item for some reason. Polled " + poll);
    }
    private void CreateDropableItems()
    {
        _dropableItems = new List<DropableItemEntry>(_dropableItemEntries.OrderByDescending(x => x.dropChance));

        float sum = _dropableItems.Sum(x => x.dropChance);

        for (int i = 0; i < _dropableItems.Count; i++)
        {
            DropableItemEntry entry = _dropableItems[i];

            entry.dropChance /= sum;

            _dropableItems[i] = entry;
        }
    }
}
[System.Serializable]
public struct DropableItemEntry
{
    public ItemBase item;
    public float dropChance;
}