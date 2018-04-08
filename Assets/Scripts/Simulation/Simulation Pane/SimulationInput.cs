using UnityEngine;

public class SimulationInput : MouseInputArea {

    static public SimulationInput instance { get; private set; }

    public bool GetEscapeKeyDown() {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    void Awake() {
        instance = (SimulationInput)Singleton.Setup(this, instance);
        collider = GetComponent<Collider2D>();
    }
}
