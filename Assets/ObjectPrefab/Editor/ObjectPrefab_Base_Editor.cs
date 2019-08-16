using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(ObjectPrefab_Base), true)]
public class ObjectPrefab_Base_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ObjectPrefab_Base ObjectPrefab = target as ObjectPrefab_Base;
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("작동"))
            ObjectPrefab.SetState(true);
        if (GUILayout.Button("비활성화"))
            ObjectPrefab.SetState(false);
        EditorGUILayout.EndHorizontal();

        //EditorGUI.BeginDisabledGroup();
        //EditorGUI.EndDisabledGroup();

    }
    private void OnSceneGUI()
    {
        ObjectPrefab_Base ObjectPrefab = target as ObjectPrefab_Base;
        if (!ObjectPrefab)
            return;

        Handles.color = Color.green;
        foreach (var iter in ObjectPrefab.LinkObjects)
        {
            Handles.DrawDottedLine(ObjectPrefab.transform.position
                , iter.transform.position,0.5f);
        }

    }


}
