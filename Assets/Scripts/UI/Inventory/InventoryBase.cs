using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBase : MonoBehaviour {

    [SerializeField]
    private GameObject SlotPrefab;
    [SerializeField]
    private Transform SlotParent;

    private void Awake()
    {
        CreateSlots();
    }
    private void CreateSlots()
    {
        for (int i = 0; i < Player.Instance.Data.InventorySize; i++)
        {
            GameObject obj = Instantiate(SlotPrefab);

            obj.transform.SetParent(SlotParent);
        }
    }
}
