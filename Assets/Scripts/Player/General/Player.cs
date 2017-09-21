using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature {

	public static Player Instance { get { return PlayModeManager.Player; } }

    public new PlayerData Data { get { return (PlayerData)base.Data; } }

    public InventoryContainer ItemContainer { get { return _itemContainer; } }
    public EquipmentContainer EquipmentContainer { get { return _equipmentContainer; } }

    private InventoryContainer _itemContainer;
    private EquipmentContainer _equipmentContainer;

    protected override void Awake()
    {
        base.Awake();

        _itemContainer = new InventoryContainer(Data.InventorySize);
        _equipmentContainer = new EquipmentContainer();
    }
}
