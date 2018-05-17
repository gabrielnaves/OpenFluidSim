using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationMode : MonoBehaviour {

    public static SimulationMode instance { get; private set; }
    public enum Mode { editor, simulation }

    public EvaluationEngine evaluationEngine;
    [ViewOnly] public Mode mode;

    void Awake() {
        instance = (SimulationMode)Singleton.Setup(this, instance);
        mode = Mode.editor;
    }

    public void ChangeToEditorMode() {
        EditorInput.instance.gameObject.SetActive(true);
        ElectricSimulationEngine.instance.Stop();
        foreach (var wire in SimulationPanel.instance.GetActiveWires())
            wire.UpdateColor(Color.black);
        mode = Mode.editor;
    }

    public void ChangeToSimulationMode() {
        evaluationEngine.EvaluateCurrentSimulation();
        if (evaluationEngine.isSimulationOk) {
            SelectedObjects.instance.ClearSelection();
            EditorInput.instance.gameObject.SetActive(false);
            ElectricSimulationEngine.instance.Setup();
            mode = Mode.simulation;
        }
    }
}
