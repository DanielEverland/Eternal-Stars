using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBase : MonoBehaviour {
    
    [SerializeField]
    private Transform SlotParent;
    [SerializeField]
    private GridLayoutGroup gridLayout;
    [SerializeField]
    private RectTransform itemIconContainer;

    private ContainerBase Container { get { return Player.Instance.ItemContainer; } }
    private Dictionary<Vector2, SlotBase> Slots = new Dictionary<Vector2, SlotBase>();
    private List<ItemIconElement> Icons = new List<ItemIconElement>();
    private List<GameObject> PoolObjects = new List<GameObject>();

    public const int SLOT_SIZE = 40;
    public const int ELEMENT_SPACING = -1;

    private void Awake()
    {
        CreateSlots();
        AssignProperties();
        
        Container.OnContainerChanged += UpdateItemIcons;
    }
    private void Start()
    {
        Canvas.ForceUpdateCanvases();

        Refresh();
    }
    public void Refresh()
    {
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

        newElement.Initialize(stack, Refresh);
        
        Slots[position].AssignIcon(newElement);

        newElement.transform.SetParent(itemIconContainer);
        newElement.transform.localPosition = Slots[position].transform.localPosition;
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
        gridLayout.cellSize = new Vector2(SLOT_SIZE, SLOT_SIZE);
        gridLayout.spacing = new Vector2(ELEMENT_SPACING, ELEMENT_SPACING);
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

            GameObject obj = PlayModeObjectPool.Pool.GetObject("InventorySlot");
            PoolObjects.Add(obj);

            obj.transform.SetParent(SlotParent);

            SlotBase slot = obj.GetComponent<SlotBase>();
            slot.Initialize(Player.Instance.ItemContainer, position);

            Slots.Add(position, slot);
        }
    }
    private void OnDestroy()
    {
        ClearItemIcons();

        for (int i = 0; i < PoolObjects.Count; i++)
        {
            PlayModeObjectPool.Pool.ReturnObject(PoolObjects[i]);
        }

        Container.OnContainerChanged -= UpdateItemIcons;
    }
}
