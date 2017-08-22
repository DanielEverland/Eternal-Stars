using System;
using UnityEngine;

public interface ScriptableObjectManager {

    void CreateObject(Type type);
    void RemoveObject(ScriptableObject source);
    void ChangeObjectType(ScriptableObject source, Type target);
}
