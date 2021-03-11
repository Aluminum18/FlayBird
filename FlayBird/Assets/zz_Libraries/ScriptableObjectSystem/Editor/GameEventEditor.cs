using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameEvent))]
public class GameEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var myTarget = (GameEvent)target;

        if (GUILayout.Button("Raise"))
        {
            myTarget.Raise();
        }
    }
}
