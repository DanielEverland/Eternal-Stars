using System;
using UnityEngine;

public interface ScriptableObjectManager {

    void CreateObject(Type type);
    void ChangeObjectType(ScriptableObject source, Type target);
}
