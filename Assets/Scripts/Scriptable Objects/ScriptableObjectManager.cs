using System;
using UnityEngine;

public interface ScriptableObjectManager<T> {

    void CreateObject(Type type);
    void RemoveObject(ScriptableObject source);
}
