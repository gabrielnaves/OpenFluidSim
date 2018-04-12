using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SelectedObjects))]
public class SelectedObjectsInspector : Editor {

    SelectedObjects selectedObjects;

    void OnEnable() {
        selectedObjects = target as SelectedObjects;
    }

    public override bool RequiresConstantRepaint() {
        return true;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if (Application.isPlaying) {
            ISelectable[] objects = selectedObjects.GetSelectedObjects();
            if (objects.Length == 0)
                EditorGUILayout.LabelField("No selected objects");
            else {
                EditorGUILayout.LabelField(objects.Length + " selected objects");
                foreach (var obj in objects)
                    if (obj is MonoBehaviour)
                        EditorGUILayout.ObjectField(obj as MonoBehaviour, typeof(MonoBehaviour), allowSceneObjects:true);
                    else
                        EditorGUILayout.LabelField("Object: " + obj);
            }
        }
    }
}
