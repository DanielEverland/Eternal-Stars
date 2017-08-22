using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CustomEditor(typeof(ConsumableItem))]
public class ConsumableItemEditor : Editor {

    private ConsumableItem Target { get { return (ConsumableItem)target; } }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EG_EditorUtility.DrawHeader("Consumable Item");

        DrawConsumableItem();
    }
    private void DrawConsumableItem()
    {
        Target.OnConsumeActions = EG_EditorUtility.DrawScriptableObjectList(Target.OnConsumeActions, Target);

        EditorUtility.SetDirty(Target);
    }
}
