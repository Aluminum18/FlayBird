using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LoadNewSceneRequester))]
public class LoadNewSceneRequesterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var myTarget = (LoadNewSceneRequester)target;
        if (GUILayout.Button("Change Scene"))
        {
            myTarget.LoadNextScene();
        }
    }
}
