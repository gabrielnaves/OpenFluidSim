using UnityEngine;

/// <summary>
/// Handles input within the simulation pane
/// </summary>
/// This class extends MouseInputArea's functionality via inheritance.
/// Unlike the base class, this one is inteded to be unique on scene,
/// and is a singleton.
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
