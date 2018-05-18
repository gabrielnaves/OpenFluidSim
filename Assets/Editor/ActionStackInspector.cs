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
        if (Application.isPlaying) {
            EditorGUILayout.Space();
            DrawActionStack("Action stack", actionStack.actionStack.ToArray());
            EditorGUILayout.Space();
            DrawActionStack("Redo stack", actionStack.redoStack.ToArray());
        }
    }

    void DrawActionStack(string title, IAction[] actions) {
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
