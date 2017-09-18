using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class ItemTriggerManager {

    public static List<Type> AvailableTriggers
    {
        get
        {
            if(_availableTriggers == null)
            {
                CreateActionReferences();
            }

            return _availableTriggers;
        }
    }
    private static List<Type> _availableTriggers;
    
    [UnityEditor.Callbacks.DidReloadScripts]
    private static void OnScriptsReloaded()
    {
        CreateActionReferences();
    }
    private static void CreateActionReferences()
    {
        _availableTriggers = new List<Type>();

        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            IEnumerable<Type> actions = assembly.GetTypes().Where(x => typeof(ItemTrigger).IsAssignableFrom(x) && !x.IsAbstract);

            _availableTriggers.AddRange(actions);
        }
    }
}
