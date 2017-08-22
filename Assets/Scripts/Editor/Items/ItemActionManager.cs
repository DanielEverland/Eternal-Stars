using System.Linq;
using System.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemActionManager {

    public static List<Type> AvailableActions { get; set; }

    [UnityEditor.Callbacks.DidReloadScripts]
    private static void OnScriptsReloaded()
    {
        CreateActionReferences();
    }
    private static void CreateActionReferences()
    {
        AvailableActions = new List<Type>();

        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            IEnumerable<Type> actions = assembly.GetTypes().Where(x => typeof(ItemAction).IsAssignableFrom(x) && !x.IsAbstract);

            AvailableActions.AddRange(actions);
        }
    }
}