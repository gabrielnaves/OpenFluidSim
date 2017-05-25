using UnityEngine;

public class SimulationInput : MonoBehaviour {

    static public SimulationInput instance { get; private set; }

    public MouseInputArea inputArea;

    public bool GetMouseButton() {
        return inputArea.mouseButton;
    }

    public bool GetMouseButtonDown() {
        return inputArea.mouseButtonDown;
    }

    public bool GetMouseButtonUp() {
        return inputArea.mouseButtonUp;
    }

    public Vector2 GetMousePosition() {
        return inputArea.GetMousePosition();
    }

    void Awake() {
        instance = (SimulationInput)Singleton.Setup(this, instance);
    }
}
