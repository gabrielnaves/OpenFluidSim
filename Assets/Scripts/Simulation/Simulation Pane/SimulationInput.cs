using UnityEngine;

public class SimulationInput : MonoBehaviour {

    static public SimulationInput instance { get; private set; }

    public MouseInputArea inputArea;

    public bool GetMouseButtonDown() {
        return inputArea.mouseButtonDown;
    }

    void Awake() {
        instance = (SimulationInput)Singleton.Setup(this, instance);
    }
}
