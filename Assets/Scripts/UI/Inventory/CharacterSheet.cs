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
        { new SubMenu("Weapons", EquipmentTypes.Weapon) },
        { new SubMenu("Implants", EquipmentTypes.Implant) },
    };

    private List<CharacterSheetSubmenu> SheetSubmenus = new List<CharacterSheetSubmenu>();
    private List<ItemIconElement> Icons = new List<ItemIconElement>();

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

        Player.Instance.EquipmentContainer.OnUpdate += CreateItemIcons;
        CreateItemIcons();
    }
    private void CreateItemIcons()
    {
        ClearItemIcons();

        for (int i = 0; i < SheetSubmenus.Count; i++)
        {
            CharacterSheetSubmenu subMenu = SheetSubmenus[i];
            
            for (int y = 0; y < subMenu.Slots.Count; y++)
            {
                EquipmentSlotEntry entry = subMenu.Slots[y];

                if (Player.Instance.EquipmentContainer.Contains(entry.Identifier))
                {
                    ItemStack stack = Player.Instance.EquipmentContainer.GetStack(entry.Identifier);

                    ItemIconElement newElement = PlayModeObjectPool.Pool.GetObject("ItemIconElement").GetComponent<ItemIconElement>();
                    Icons.Add(newElement);

                    newElement.Initialize(stack, CreateItemIcons);

                    entry.Slot.AssignIcon(newElement);

                    newElement.transform.SetParent(entry.Slot.transform);
                    newElement.transform.localPosition = Vector3.zero;
                }
            }
        }        
    }
    private void ClearItemIcons()
    {
        for (int i = 0; i < Icons.Count; i++)
        {
            PlayModeObjectPool.Pool.ReturnObject(Icons[i].gameObject);
        }

        Icons.Clear();
    }
    private void CreateSubmenu(SubMenu subMenu)
    {
        CharacterSheetSubmenu obj = Instantiate(subMenuPrefab);

        obj.transform.SetParent(subMenuParent);

        obj.Initialize(subMenu);

        SheetSubmenus.Add(obj);
    }
    private void OnDestroy()
    {
        ClearItemIcons();
    }

    public class SubMenu
    {
        private SubMenu() { }

        public SubMenu(string headerName, EquipmentTypes acceptsEquipmentType)
        {
            _headerName = headerName;
            _compatibleEquipmentType = acceptsEquipmentType;
        }

        public string HeaderName { get { return _headerName; } }
        public EquipmentTypes EquipmentType { get { return _compatibleEquipmentType; } }
        
        private readonly string _headerName;
        private readonly EquipmentTypes _compatibleEquipmentType;

        public bool Contains(EquipmentSlotIdentifier identifier)
        {
            return identifier.EquipmentType == _compatibleEquipmentType;
        }
    }
}
