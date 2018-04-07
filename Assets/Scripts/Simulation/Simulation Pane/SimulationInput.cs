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

    public bool GetRightMouseDown() {
        return inputArea.rightMouseButtonDown;
    }
    
    /// <summary>
    /// Returns mouse position as world point
    /// </summary>
    public Vector2 GetMousePosition() {
        return inputArea.mousePosition;
    }

    public bool IsMouseOnInputArea() {
        return inputArea.IsMouseInsideInputArea();
    }
    
    public bool GetEscapeKeyDown() {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    void Awake() {
        instance = (SimulationInput)Singleton.Setup(this, instance);
    }
}
