using UnityEditorInternal;
using System;
using UnityEngine;
using System.Collections.Generic;

public interface ScriptableObjectManager<T> {

    string ListHeader { get; }
    ReorderableList ReorderableList { get; }
    List<Type> AvailableTypes { get; }

    void CreateObject(Type type);
    void RemoveObject(ScriptableObject source);
}
