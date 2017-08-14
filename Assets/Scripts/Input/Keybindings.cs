using ProtoBuf;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Keybindings
{
    public static IEnumerable<KeybindingElement> KeybindingElements { get; private set; }

    private static Dictionary<string, KeybindingElement> IndexedElements { get; set; }
    
    private static readonly List<KeybindingElement> DefaultKeyCode = new List<KeybindingElement>()
    {
        //-------------MOVEMENT-------------
        new KeybindingElement("Move Up", "Movement")
        {
            PrimaryKey = (int)KeyCode.W,
        },
        new KeybindingElement("Move Down", "Movement")
        {
            PrimaryKey = (int)KeyCode.S,
        },
        new KeybindingElement("Move Left", "Movement")
        {
            PrimaryKey = (int)KeyCode.A,
        },
        new KeybindingElement("Move Right", "Movement")
        {
            PrimaryKey = (int)KeyCode.D,
        },

        //-------------TOOLBAR-------------
        new KeybindingElement("Action Button 1", "Toolbar")
        {
            PrimaryKey = (int)KeyCode.Alpha1,
        },
        new KeybindingElement("Action Button 2", "Toolbar")
        {
            PrimaryKey = (int)KeyCode.Alpha2,
        },
        new KeybindingElement("Action Button 3", "Toolbar")
        {
            PrimaryKey = (int)KeyCode.Alpha3,
        },
        new KeybindingElement("Action Button 4", "Toolbar")
        {
            PrimaryKey = (int)KeyCode.Alpha4,
        },
        new KeybindingElement("Action Button 5", "Toolbar")
        {
            PrimaryKey = (int)KeyCode.Alpha5,
        },
        new KeybindingElement("Action Button 6", "Toolbar")
        {
            PrimaryKey = (int)KeyCode.Alpha6,
        },
        new KeybindingElement("Action Button 7", "Toolbar")
        {
            PrimaryKey = (int)KeyCode.Alpha7,
        },
        new KeybindingElement("Action Button 8", "Toolbar")
        {
            PrimaryKey = (int)KeyCode.Alpha8,
        },
        new KeybindingElement("Action Button 9", "Toolbar")
        {
            PrimaryKey = (int)KeyCode.Alpha9,
        },
        new KeybindingElement("Action Button 10", "Toolbar")
        {
            PrimaryKey = (int)KeyCode.Alpha0,
        },
        new KeybindingElement("Action Button 11", "Toolbar")
        {

        },
        new KeybindingElement("Action Button 12", "Toolbar")
        {

        },

        //-------------COMBAT-------------
        new KeybindingElement("Shoot", "Combat")
        {
            PrimaryKey = (int)KeyCode.Mouse0,
        },
        new KeybindingElement("Cancel Spell Cast", "Combat")
        {
            PrimaryKey = (int)KeyCode.Escape,
        },
        new KeybindingElement("Reload", "Combat")
        {
            PrimaryKey = (int)KeyCode.R,
        },

        //-------------COMMANDS-------------
        new KeybindingElement("CommandConsoleFocus", "Commands")
        {
            PrimaryKey = (int)KeyCode.T,
        },
        new KeybindingElement("Interact", "Commands")
        {
            PrimaryKey = (int)KeyCode.E,
            SecondaryKey = (int)KeyCode.Keypad5,
        },
        new KeybindingElement("Search", "Commands")
        {
            PrimaryKey = (int)KeyCode.S,
            PrimaryModifier = (int)KeyCode.LeftShift,
        },
        new KeybindingElement("Wait", "Commands")
        {
            PrimaryKey = (int)KeyCode.W,
            PrimaryModifier = (int)KeyCode.LeftShift,
        },
        new KeybindingElement("Pick Up All Items", "Commands")
        {
            PrimaryKey = (int)KeyCode.F,
        },

        //-------------UI-------------
        new KeybindingElement("Open Inventory", "UI")
        {
            PrimaryKey = (int)KeyCode.I,
        },
        new KeybindingElement("Open Character Sheet", "UI")
        {
            PrimaryKey = (int)KeyCode.C,
        },

    };
    public static void Load()
    {
        KeybindingElements = DefaultKeyCode;

        //Implement loading of user assigned KeyCode here

        IndexElements();
    }
    private static void IndexElements()
    {
        IndexedElements = new Dictionary<string, KeybindingElement>();

        foreach (KeybindingElement element in KeybindingElements)
        {
            IndexedElements.Add(element.Name, element);
        }
    }
    public static bool GetKey(string key)
    {
        return IndexedElements[key].IsPressed(Input.GetKey);
    }
    public static bool GetKeyDown(string key)
    {
        return IndexedElements[key].IsPressed(Input.GetKeyDown);
    }
    public static bool GetKeyUp(string key)
    {
        return IndexedElements[key].IsPressed(Input.GetKeyUp);
    }
    public static KeybindingElement GetElement(string key)
    {
        return IndexedElements[key];
    }
}
[ProtoContract]
public class KeybindingElement
{
    public KeybindingElement(string name, string group)
    {
        _name = name;
        _group = group;
    }

    public string Name { get { return _name; } }
    [ProtoMember(1)]
    private readonly string _name;

    public string Group { get { return _group; } }
    [ProtoMember(2)]
    private readonly string _group;

    public int? PrimaryKey;
    public int? PrimaryModifier;

    public int? SecondaryKey;
    public int? SecondaryModifier;

    public bool IsPressed(Func<KeyCode, bool> function)
    {
        if (PollKeyModifierPair(PrimaryKey, PrimaryModifier, function))
        {
            return true;
        }

        if (PollKeyModifierPair(SecondaryKey, SecondaryModifier, function))
        {
            return true;
        }

        return false;
    }
    private bool PollKeyModifierPair(int? key, int? modifier, Func<KeyCode, bool> function)
    {
        if (key.HasValue)
        {
            if (modifier.HasValue)
            {
                if (function((KeyCode)key) && Input.GetKey((KeyCode)modifier))
                    return true;
            }
            else
            {
                if (function((KeyCode)key))
                    return true;
            }
        }

        return false;
    }
}
