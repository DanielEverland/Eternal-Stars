using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;

public class ImplantItem : EquipableItem, ScriptableObjectManager<ItemTrigger>, ScriptableObjectManager<ItemAction> {

    public override string ItemType { get { return "Implant"; } }
    public override EquipmentTypes EquipmentType { get { return EquipmentTypes.Implant; } }

    [SerializeField, Range(0, 1)]
    private float _procChance;
    [SerializeField]
    private List<ItemTrigger> _procTriggers;
    [SerializeField]
    private List<ItemAction> _procActions;

    public override void OnCreatedInInspector()
    {
        _procTriggers = new List<ItemTrigger>();
        _procActions = new List<ItemAction>();
    }

    public override string Description
    {
        get
        {
            return base.Description.Replace("%", Mathf.RoundToInt(_procChance * 100) + "%");
        }
    }

    public override string GetTooltipContent()
    {
        return base.GetTooltipContent() + "\n" + Description;
    }

    private void OnTrigger()
    {
        float randomNumber = UnityEngine.Random.Range(0, 1);

        if (randomNumber > _procChance)
            return;

        for (int i = 0; i < _procActions.Count; i++)
        {
            _procActions[i].DoAction();
        }
    }
    protected override void OnEquipped(ItemStack stack)
    {
        for (int i = 0; i < _procTriggers.Count; i++)
        {
            _procTriggers[i].OnSubscribe();
            _procTriggers[i].OnTrigger += OnTrigger;
        }
    }
    protected override void OnUnequipped(ItemStack stack)
    {
        for (int i = 0; i < _procTriggers.Count; i++)
        {
            _procTriggers[i].OnUnsubscribe();
            _procTriggers[i].OnTrigger -= OnTrigger;
        }
    }

#if UNITY_EDITOR
    string ScriptableObjectManager<ItemAction>.ListHeader { get { return "Proc Actions"; } }
    List<Type> ScriptableObjectManager<ItemAction>.AvailableTypes { get { return ItemActionManager.AvailableActions; } }
    ReorderableList ScriptableObjectManager<ItemAction>.ReorderableList
    {
        get
        {
            if (itemActionReorderableList == null)
            {
                itemActionReorderableList = new ReorderableList(_procActions, typeof(ItemAction));
            }

            return itemActionReorderableList;
        }
    }
    string ScriptableObjectManager<ItemTrigger>.ListHeader { get { return "Proc Triggers"; } }
    List<Type> ScriptableObjectManager<ItemTrigger>.AvailableTypes { get { return ItemTriggerManager.AvailableTriggers; } }
    ReorderableList ScriptableObjectManager<ItemTrigger>.ReorderableList
    {
        get
        {
            if (itemTriggerReorderableList == null)
            {
                itemTriggerReorderableList = new ReorderableList(_procTriggers, typeof(ItemAction));
            }

            return itemTriggerReorderableList;
        }
    }
    private ReorderableList itemActionReorderableList;
    private ReorderableList itemTriggerReorderableList;

    [MenuItem("Assets/Create/Items/Implant", priority = Utility.CREATE_ASSET_ORDER_ID)]
    private static void CreateAssetImplant()
    {
        Utility.CreateItemAndRename<ImplantItem>();
    }
    void ScriptableObjectManager<ItemAction>.CreateObject(Type type)
    {
        _procActions.Add(Utility.CreateObject<ItemAction>(type, this));
    }
    void ScriptableObjectManager<ItemTrigger>.CreateObject(Type type)
    {
        _procTriggers.Add(Utility.CreateObject<ItemTrigger>(type, this));
    }
    void ScriptableObjectManager<ItemAction>.RemoveObject(ScriptableObject source)
    {
        int index = _procActions.IndexOf((ItemAction)source);

        DestroyImmediate(source, true);

        _procActions.RemoveAt(index);
    }
    void ScriptableObjectManager<ItemTrigger>.RemoveObject(ScriptableObject source)
    {
        int index = _procTriggers.IndexOf((ItemTrigger)source);

        DestroyImmediate(source, true);

        _procTriggers.RemoveAt(index);
    }
#endif
}
