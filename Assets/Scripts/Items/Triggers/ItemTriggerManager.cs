using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class ItemTriggerManager {

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
            IEnumerable<Type> actions = assembly.GetTypes().Where(x => typeof(ItemTrigger).IsAssignableFrom(x) && !x.IsAbstract);

            AvailableActions.AddRange(actions);
        }
    }
}
