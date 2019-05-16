using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CircleBorderMesh))]
public class CircleRendererEditor : Editor
{
    private CircleBorderMesh _instance;

    private void OnEnable()
    {
        _instance = (CircleBorderMesh)target;
    }

    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();
            if (check.changed)
            {
                _instance.OnEditorSettingsChanged();
            }
        }
        if (GUILayout.Button("Generate Circle"))
        {
            _instance.Generate();
        }
    }
}
