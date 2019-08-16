using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//maskInteraction//https://docs.unity3d.com/ScriptReference/Editor.html
//https://docs.unity3d.com/kr/current/Manual/class-SpriteRenderer.html
//https://docs.unity3d.com/kr/current/ScriptReference/SpriteRenderer.html
[CustomEditor(typeof(SpriteRenderer), true)]
public class SpriteRendererEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Extern SpriteRenderer Inspector");
        //base.OnInspectorGUI();
        DrawDefaultInspector();
    }
}

