using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(IntegerListVariable))]
public class IntegerListVariableEditor : Editor
{
    private SerializedProperty _listValue;
    private SerializedProperty _dontDuplicateValue;

    private void OnEnable()
    {
        _listValue = serializedObject.FindProperty("_list");
        _dontDuplicateValue = serializedObject.FindProperty("_dontDuplicateValue");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_listValue);
        EditorGUILayout.PropertyField(_dontDuplicateValue);

        var myTarget = (IntegerListVariable)target;

        int size = _listValue.arraySize;
        List<int> valueList = new List<int>();
        for (int i = 0; i < size; i++)
        {
            valueList.Add(_listValue.GetArrayElementAtIndex(i).intValue);
        }

        myTarget.CompareWithNewList(valueList);

        serializedObject.ApplyModifiedProperties();
    }
}
