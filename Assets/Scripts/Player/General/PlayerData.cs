using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData.asset", menuName = "Creature/Player Data", order = Utility.CREATE_ASSET_ORDER_ID)]
public class PlayerData : CreatureData {

    public int InventorySize { get { return _defaultInventorySize; } }

    [SerializeField]
    private int _defaultInventorySize = 32;
}
