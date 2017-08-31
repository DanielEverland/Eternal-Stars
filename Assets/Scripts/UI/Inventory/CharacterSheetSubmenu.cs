using System.Linq;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSheetSubmenu : MonoBehaviour {

    [SerializeField]
    private TMP_Text header;
    [SerializeField]
    private RectTransform slotParent;

    public void Initialize(CharacterSheet.SubMenu submenu)
    {
        header.text = submenu.HeaderName;

        for (int i = 0; i < submenu.SlotTypes.Count(); i++)
        {
            GameObject obj = PlayModeObjectPool.Pool.GetObject("InventorySlot");
            obj.transform.SetParent(slotParent);

            SlotBase slot = obj.GetComponent<SlotBase>();

            slot.Initialize(Player.Instance.EquipmentContainer, submenu.GetObject(i));
        }
    }
}
