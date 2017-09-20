using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CustomEditor(typeof(WeaponBase))]
public class WeaponBaseEditor : Editor {

    private WeaponBase Target { get { return (WeaponBase)target; } }

    public override void OnInspectorGUI()
    {
        EG_EditorUtility.DrawWeaponUI(Target, serializedObject);
    }
}
