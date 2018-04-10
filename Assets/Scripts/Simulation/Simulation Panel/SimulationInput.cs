using UnityEngine;

/// <summary>
/// Handles input within the simulation pane
/// </summary>
/// This class extends MouseInputArea's functionality via inheritance.
/// Unlike the base class, this one is inteded to be unique on scene,
/// and is a singleton.
public class SimulationInput : MouseInputArea {

    static public SimulationInput instance { get; private set; }

    public float holdDistance = 0.1f;
    public float minHoldTime = 0.3f;

    public bool singleClick { get; private set; }
    public bool mouseHold { get; private set; }

    public bool GetEscapeKeyDown() {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    void Awake() {
        instance = (SimulationInput)Singleton.Setup(this, instance);
        collider = GetComponent<Collider2D>();
    }

    bool clicked = false;
    Vector2 mousePositionOnClick;

    void LateUpdate() {
        if (singleClick)
            singleClick = false;

        if (mouseButtonDown) {
            mousePositionOnClick = mousePosition;
            clicked = true;
        }

        if (mouseButtonUp && clicked) {
            if (!mouseHold)
                singleClick = true;
            mouseHold = false;
            clicked = false;
        }

        if (clicked) {
            if (Vector2.Distance(mousePosition, mousePositionOnClick) > holdDistance)
                mouseHold = true;
        }
    }

    bool Equal(Vector2 a, Vector2 b) {
        return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y);
    }
}
