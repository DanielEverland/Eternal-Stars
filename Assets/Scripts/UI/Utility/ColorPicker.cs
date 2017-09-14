using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicker : MonoBehaviour {

    [SerializeField]
    private Graphic Target;
    [SerializeField]
    private string Key;

    private void Awake()
    {
        AssignColor();
    }
    private void OnValidate()
    {
        if (Target == null)
            return;

        AssignColor();
    }
    private void AssignColor()
    {
        Target.color = UIColors.GetColor(Key);
    }
#if UNITY_EDITOR
    [ContextMenu("Select Color Manager")]
    private void SelectColorManager()
    {
        UnityEditor.Selection.activeObject = UIColors.Instance;
    }
#endif
}
