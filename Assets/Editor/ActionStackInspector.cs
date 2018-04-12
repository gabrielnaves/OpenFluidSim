using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ActionStack))]
public class ActionStackInspector : Editor {

    ActionStack actionStack;

    void OnEnable() {
        actionStack = target as ActionStack;
    }

    public override bool RequiresConstantRepaint() {
        return true;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        EditorGUILayout.Space();
        DrawActionStack("Action Stack", actionStack.GetActionStack());
        EditorGUILayout.Space();
        DrawActionStack("Redo Stack", actionStack.GetRedoStack());
    }

    void DrawActionStack(string title, IAction[] actions) {
        if (actions.Length == 0)
            title += " empty";
        else
            title += ": " + actions.Length;
        EditorGUILayout.LabelField(title);

        int amountDrawn = 0;
        foreach (var action in actions) {
            EditorGUILayout.LabelField(" - " + action.Name());
            if (++amountDrawn >= 10)
                break;
        }
    }
}
