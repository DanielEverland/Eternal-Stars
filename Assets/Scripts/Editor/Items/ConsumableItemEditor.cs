using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CustomEditor(typeof(ConsumableItem))]
public class ConsumableItemEditor : Editor {

    private ConsumableItem Target { get { return (ConsumableItem)target; } }

    public override void OnInspectorGUI()
    {
        EG_EditorUtility.DrawItemBaseUI(EditorGUILayout.GetControlRect(), Target, serializedObject);
        

        DrawConsumableItem();
    }
    private void DrawConsumableItem()
    {
        Target.OnConsumeActions = EG_EditorUtility.DrawScriptableObjectList("On Consume Actions", Target.OnConsumeActions, ItemActionManager.AvailableActions, Target);

        EditorUtility.SetDirty(Target);
    }
}
