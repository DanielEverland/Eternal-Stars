using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionManager : MonoBehaviour {

    [SerializeField]
    private WeaponSelectionElement _selectionElement;
    [SerializeField]
    private RectTransform _elementParent;

    private EquipmentContainer EquipmentContainer { get { return Player.Instance.EquipmentContainer; } }
    private readonly Dictionary<int, string> Keybindings = new Dictionary<int, string>()
    {
        { 0, "Select Weapon 1" },
        { 1, "Select Weapon 2" },
        { 2, "Select Weapon 3" },
    };
    
    private void Start()
    {
        InitializeElements();

        Player.Instance.EquipmentContainer.OnUpdate += InitializeElements;
    }
    private void InitializeElements()
    {
        CleanupElements();

        for (int i = 0; i < Keybindings.Count; i++)
        {
            WeaponSelectionElement element = GetElement();
            EquipmentSlotIdentifier identifier = new EquipmentSlotIdentifier(EquipmentTypes.Weapon, (byte)i);
            WeaponBase weapon = null;

            if (EquipmentContainer.Contains(identifier))
            {
                weapon = EquipmentContainer.GetItem(identifier) as WeaponBase;
            }

            element.Initialize(Keybindings[i], weapon);
        }
    }
    private WeaponSelectionElement GetElement()
    {
        WeaponSelectionElement element = Instantiate(_selectionElement);
        element.transform.SetParent(_elementParent);

        return element;
    }
    private void CleanupElements()
    {
        foreach (Transform transform in _elementParent.transform)
        {
            GameObject.Destroy(transform.gameObject);
        }
    }
}
