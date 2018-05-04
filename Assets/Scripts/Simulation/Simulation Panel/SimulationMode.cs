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
        Debug.Log("Change to editor mode not implemented");
    }

    public void ChangeToSimulationMode() {
        evaluationEngine.EvaluateCurrentSimulation();
        if (evaluationEngine.isSimulationOk) {
            SelectedObjects.instance.ClearSelection();
            SimulationInput.instance.gameObject.SetActive(false);
            mode = Mode.simulation;
        }
    }
}
