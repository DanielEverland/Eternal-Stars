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

    public List<EquipmentSlotEntry> Slots = new List<EquipmentSlotEntry>();

    public void Initialize(CharacterSheet.SubMenu submenu)
    {
        header.text = submenu.HeaderName;

        for (byte i = 0; i < submenu.SlotAmounts; i++)
        {
            GameObject obj = PlayModeObjectPool.Pool.GetObject("InventorySlot");
            obj.transform.SetParent(slotParent);

            SlotBase slot = obj.GetComponent<SlotBase>();

            EquipmentSlotIdentifier identifier = new EquipmentSlotIdentifier()
            {
                EquipmentType = submenu.EquipmentType,
                Index = i,
            };

            slot.Initialize(Player.Instance.EquipmentContainer, identifier);
            
            EquipmentSlotEntry entry = new EquipmentSlotEntry()
            {
                Identifier = identifier,
                Slot = slot,
            };

            Slots.Add(entry);
        }
    }
}
public struct EquipmentSlotEntry
{
    public EquipmentSlotIdentifier Identifier;
    public SlotBase Slot;
}
