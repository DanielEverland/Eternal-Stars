using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSheet : MonoBehaviour {

    [SerializeField]
    private CharacterSheetSubmenu subMenuPrefab;
    [SerializeField]
    private Transform subMenuParent;

    public static IEnumerable<SubMenu> SubMenus { get { return subMenus; } }
    private static readonly List<SubMenu> subMenus = new List<SubMenu>()
    {
        { new SubMenu("Weapons", EquipmentTypes.Weapon, EquipmentSlotTypes.Weapon1, EquipmentSlotTypes.Weapon2, EquipmentSlotTypes.Weapon3) },
        { new SubMenu("Implants", EquipmentTypes.Implant, EquipmentSlotTypes.Implant1, EquipmentSlotTypes.Implant2, EquipmentSlotTypes.Implant3) },
    };

    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        for (int i = 0; i < subMenus.Count; i++)
        {
            CreateSubmenu(subMenus[i]);
        }
    }
    private void CreateSubmenu(SubMenu subMenu)
    {
        CharacterSheetSubmenu obj = Instantiate(subMenuPrefab);

        obj.transform.SetParent(subMenuParent);

        obj.Initialize(subMenu);
    }

    public class SubMenu
    {
        private SubMenu() { }

        public SubMenu(string headerName, EquipmentTypes acceptsEquipmentType, params EquipmentSlotTypes[] slotTypes)
        {
            _headerName = headerName;
            _compatibleEquipmentType = acceptsEquipmentType;
            _slotTypes = new List<EquipmentSlotTypes>(slotTypes);
        }

        public string HeaderName { get { return _headerName; } }
        public EquipmentTypes CompatibleEquipmentType { get { return _compatibleEquipmentType; } }
        public IEnumerable<EquipmentSlotTypes> SlotTypes { get { return _slotTypes; } }
        
        private readonly string _headerName;
        private readonly List<EquipmentSlotTypes> _slotTypes;
        private readonly EquipmentTypes _compatibleEquipmentType;

        public EquipmentSlotTypes GetObject(int index)
        {
            return _slotTypes[index];
        }
        public bool Contains(EquipmentSlotTypes slotType)
        {
            return _slotTypes.Contains(slotType);
        }
    }
}
