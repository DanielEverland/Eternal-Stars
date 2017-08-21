using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBase : MonoBehaviour {

    [SerializeField]
    private GameObject SlotPrefab;
    [SerializeField]
    private Transform SlotParent;
    [SerializeField]
    private GridLayoutGroup gridLayout;
    [SerializeField]
    private Transform iconParent;

    private ContainerBase Container { get { return Player.Instance.Container; } }
    private Dictionary<Vector2, SlotBase> Slots = new Dictionary<Vector2, SlotBase>();
    private List<ItemIconElement> Icons = new List<ItemIconElement>();

    public const int ELEMENT_SIZE = 40;

    private void Awake()
    {
        CreateSlots();
        AssignProperties();
        
        Container.OnContainerChanged += UpdateItemIcons;
    }
    private void Start()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)gridLayout.transform);

        UpdateItemIcons();
    }
    private void UpdateItemIcons()
    {
        ClearItemIcons();

        foreach (KeyValuePair<Vector2, ItemStack> pair in Container.Items)
        {
            CreateItemIcon(pair.Key, pair.Value);
        }
    }
    private void CreateItemIcon(Vector2 position, ItemStack stack)
    {
        ItemIconElement newElement = PlayModeObjectPool.Pool.GetObject("ItemIconElement").GetComponent<ItemIconElement>();
        Icons.Add(newElement);

        newElement.Initialize(stack);

        newElement.transform.SetParent(iconParent);
        Slots[position].AssignIcon(newElement);
    }
    private void ClearItemIcons()
    {
        for (int i = 0; i < Icons.Count; i++)
        {
            PlayModeObjectPool.Pool.ReturnObject(Icons[i].gameObject);
        }

        Icons.Clear();
    }
    private void AssignProperties()
    {
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = ContainerBase.INVENTORY_COLUMNS;
        gridLayout.cellSize = new Vector2(ELEMENT_SIZE, ELEMENT_SIZE);
    }
    private void CreateSlots()
    {
        for (int i = 0; i < Container.Size; i++)
        {
            Vector2 position = new Vector2()
            {
                x = i % ContainerBase.INVENTORY_COLUMNS,
                y = (Mathf.CeilToInt((i + 1f) / (float)ContainerBase.INVENTORY_COLUMNS) - 1),
            };

            GameObject obj = Instantiate(SlotPrefab);

            obj.transform.SetParent(SlotParent);

            SlotBase slot = obj.GetComponent<SlotBase>();
            slot.Initialize(this);

            Slots.Add(position, slot);
        }
    }
    private void OnDestroy()
    {
        ClearItemIcons();
    }
}
