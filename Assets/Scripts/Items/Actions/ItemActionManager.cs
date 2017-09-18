using System.Linq;
using System.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemActionManager {

    public static List<Type> AvailableActions
    {
        get
        {
            if (_availableActions == null)
            {
                CreateActionReferences();
            }

            return _availableActions;
        }
    }
    private static List<Type> _availableActions;

    [UnityEditor.Callbacks.DidReloadScripts]
    private static void OnScriptsReloaded()
    {
        CreateActionReferences();
    }
    private static void CreateActionReferences()
    {
        _availableActions = new List<Type>();

        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            IEnumerable<Type> actions = assembly.GetTypes().Where(x => typeof(ItemAction).IsAssignableFrom(x) && !x.IsAbstract);

            _availableActions.AddRange(actions);
        }
    }
}