using UnityEditor.UI;
using UnityEditor.AnimatedValues;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CanEditMultipleObjects, CustomEditor(typeof(ConstrainedContentSizeFitter), true)]
public class ConstrainedContentSizeFitterEditor : SelfControllerEditor {

    private ConstrainedContentSizeFitter Target { get { return (ConstrainedContentSizeFitter)target; } }

    private AnimBool verticalFadeValue;
    private AnimBool verticalMaxFadeValue;
    private AnimBool verticalMinFadeValue;

    private AnimBool horizontalFadeValue;
    private AnimBool horizontalMaxFadeValue;
    private AnimBool horizontalMinFadeValue;

    private void OnEnable()
    {
        verticalFadeValue = new AnimBool(ShouldDrawFitOptions(Target.verticalFit));
        verticalMaxFadeValue = new AnimBool(Target.verticalUseMaxSize);
        verticalMinFadeValue = new AnimBool(Target.verticalUseMinSize);

        horizontalFadeValue = new AnimBool(ShouldDrawFitOptions(Target.horizontalFit));
        horizontalMaxFadeValue = new AnimBool(Target.horizontalUseMaxSize);
        horizontalMinFadeValue = new AnimBool(Target.horizontalUseMinSize);



        verticalFadeValue.valueChanged.AddListener(Repaint);
        verticalMaxFadeValue.valueChanged.AddListener(Repaint);
        verticalMinFadeValue.valueChanged.AddListener(Repaint);

        horizontalFadeValue.valueChanged.AddListener(Repaint);
        horizontalMaxFadeValue.valueChanged.AddListener(Repaint);
        horizontalMinFadeValue.valueChanged.AddListener(Repaint);
    }
    public override void OnInspectorGUI()
    {
        base.serializedObject.Update();

        DrawHorizontal();

        if(horizontalFadeValue.target)
            EditorGUILayout.Space();

        DrawVertical();

        base.serializedObject.ApplyModifiedProperties();
    }
    private void DrawVertical()
    {
        Target.verticalFit = (ConstrainedContentSizeFitter.FitMode)EditorGUILayout.EnumPopup("Vertical Fit", Target.verticalFit);

        EditorGUI.indentLevel++;
        if (BeginFadeGroup(ShouldDrawFitOptions(Target.verticalFit), ref verticalFadeValue))
        {
            Target.verticalUseMinSize = EditorGUILayout.Toggle("Use Min Height", Target.verticalUseMinSize);

            EditorGUI.indentLevel++;
            if (BeginFadeGroup(Target.verticalUseMinSize, ref verticalMinFadeValue))
            {
                Target.verticalMinSize = EditorGUILayout.FloatField("Min Height", Target.verticalMinSize);
            }
            EditorGUILayout.EndFadeGroup();
            EditorGUI.indentLevel--;


            Target.verticalUseMaxSize = EditorGUILayout.Toggle("Use Max Height", Target.verticalUseMaxSize);

            EditorGUI.indentLevel++;
            if (BeginFadeGroup(Target.verticalUseMaxSize, ref verticalMaxFadeValue))
            {
                Target.verticalMaxSize = EditorGUILayout.FloatField("Max Height", Target.verticalMaxSize);
            }
            EditorGUILayout.EndFadeGroup();
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFadeGroup();
        EditorGUI.indentLevel--;
    }
    private void DrawHorizontal()
    {
        Target.horizontalFit = (ConstrainedContentSizeFitter.FitMode)EditorGUILayout.EnumPopup("Horizontal Fit", Target.horizontalFit);

        EditorGUI.indentLevel++;
        if (BeginFadeGroup(ShouldDrawFitOptions(Target.horizontalFit), ref horizontalFadeValue))
        {
            Target.horizontalUseMinSize = EditorGUILayout.Toggle("Use Min Width", Target.horizontalUseMinSize);

            EditorGUI.indentLevel++;
            if (BeginFadeGroup(Target.horizontalUseMinSize, ref horizontalMinFadeValue))
            {
                Target.horizontalMinSize = EditorGUILayout.FloatField("Min Width", Target.horizontalMinSize);
            }
            EditorGUILayout.EndFadeGroup();
            EditorGUI.indentLevel--;


            Target.horizontalUseMaxSize = EditorGUILayout.Toggle("Use Max Width", Target.horizontalUseMaxSize);

            EditorGUI.indentLevel++;
            if (BeginFadeGroup(Target.horizontalUseMaxSize, ref horizontalMaxFadeValue))
            {
                Target.horizontalMaxSize = EditorGUILayout.FloatField("Max Width", Target.horizontalMaxSize);
            }
            EditorGUILayout.EndFadeGroup();
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFadeGroup();
        EditorGUI.indentLevel--;
    }
    private bool ShouldDrawFitOptions(ConstrainedContentSizeFitter.FitMode fitMode)
    {
        return fitMode == ConstrainedContentSizeFitter.FitMode.MinSize || fitMode == ConstrainedContentSizeFitter.FitMode.PreferredSize;
    }
    private bool BeginFadeGroup(bool shouldFade, ref AnimBool fadeValue)
    {
        fadeValue.target = shouldFade;

        EditorGUILayout.BeginFadeGroup(fadeValue.faded);

        return shouldFade;
    }
}
