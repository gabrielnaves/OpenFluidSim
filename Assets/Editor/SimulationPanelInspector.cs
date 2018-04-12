using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SimulationPanel))]
public class SimulationPanelInspector : Editor {

    SimulationPanel simPanel;
    static bool showSelectables = false;
    static bool showDraggables = false;
    static bool showConfigurables = false;

    void OnEnable() {
        simPanel = target as SimulationPanel;
    }

    public override bool RequiresConstantRepaint() {
        return true;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if (Application.isPlaying) {
            showSelectables = EditorGUILayout.Foldout(showSelectables, "Selectables");
            if (showSelectables) {
                GUI.enabled = false;
                ISelectable[] selectables = simPanel.GetActiveSelectables();
                ShowSizeField(selectables.Length);
                for (int i = 0; i < selectables.Length; ++i) {
                    EditorGUILayout.ObjectField(selectables[i] as MonoBehaviour, typeof(MonoBehaviour), true);
                }
                GUI.enabled = true;
            }

            showDraggables = EditorGUILayout.Foldout(showDraggables, "Draggables");
            if (showDraggables) {
                GUI.enabled = false;
                IDraggable[] draggables = simPanel.GetActiveDraggables();
                ShowSizeField(draggables.Length);
                for (int i = 0; i < draggables.Length; ++i) {
                    EditorGUILayout.ObjectField(draggables[i] as MonoBehaviour, typeof(MonoBehaviour), true);
                }
                GUI.enabled = true;
            }

            showConfigurables = EditorGUILayout.Foldout(showConfigurables, "Configurables");
            if (showConfigurables) {
                GUI.enabled = false;
                IConfigurable[] configurables = simPanel.GetActiveConfigurables();
                ShowSizeField(configurables.Length);
                for (int i = 0; i < configurables.Length; ++i) {
                    EditorGUILayout.ObjectField(configurables[i] as MonoBehaviour, typeof(MonoBehaviour), true);
                }
                GUI.enabled = true;
            }
        }
    }
    
    void ShowSizeField(int size) {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Size");
        EditorGUILayout.IntField(size);
        EditorGUILayout.EndHorizontal();
    }
}
