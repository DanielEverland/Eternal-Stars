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

    private ContainerBase Container { get { return Player.Instance.Container; } }

    private void Awake()
    {
        CreateSlots();
        AssignProperties();
    }
    private void AssignProperties()
    {
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = ContainerBase.INVENTORY_COLUMNS;
    }
    private void CreateSlots()
    {
        for (int i = 0; i < Container.Size; i++)
        {
            GameObject obj = Instantiate(SlotPrefab);

            obj.transform.SetParent(SlotParent);
        }
    }
}
