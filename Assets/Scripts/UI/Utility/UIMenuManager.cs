using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuManager : MonoBehaviour {

    [SerializeField]
    private List<MenuEntry> Entries;

    private Dictionary<string, GameObject> InstantiatedMenus = new Dictionary<string, GameObject>();

    private void Update()
    {
        for (int i = 0; i < Entries.Count; i++)
        {
            MenuEntry entry = Entries[i];

            if (Keybindings.GetKeyUp(entry.keybinding))
            {
                ToggleMenu(entry);
            }
        }
    }
    private void ToggleMenu(MenuEntry entry)
    {
        if (InstantiatedMenus.ContainsKey(entry.keybinding))
        {
            Destroy(InstantiatedMenus[entry.keybinding]);
            InstantiatedMenus.Remove(entry.keybinding);
        }
        else
        {
            GameObject obj = Instantiate(entry.prefab);
            obj.transform.SetParent(Canvas2D.Static.transform, false);
            InstantiatedMenus.Add(entry.keybinding, obj);
        }
    }

    [System.Serializable]
    private struct MenuEntry
    {
        public string keybinding;
        public GameObject prefab;
    }
}