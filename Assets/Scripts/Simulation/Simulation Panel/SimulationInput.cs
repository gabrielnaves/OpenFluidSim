using UnityEngine;

/// <summary>
/// Produces input flags specific to the simulation panel
/// </summary>
/// This class extends MouseInputArea's functionality via inheritance.
/// Unlike the base class, this one is inteded to be unique on scene,
/// and is a singleton.
public class SimulationInput : MouseInputArea {

    static public SimulationInput instance { get; private set; }

    public float holdDistance = 0.1f;

    public bool singleClick { get; private set; }
    public bool mouseDrag { get; private set; }
    public bool mouseDragStart { get; private set; }
    public bool mouseDragEnd { get; private set; }
    public Vector2 startingDragPoint { get; private set; }

    void Awake() {
        instance = (SimulationInput)Singleton.Setup(this, instance);
        collider = GetComponent<Collider2D>();
    }

    bool clicked = false;

    void Update() {
        ResetSingleFrameFlags();

        if (mouseButtonDown) {
            startingDragPoint = mousePosition;
            clicked = true;
        }

        if (mouseButtonUp && clicked) {
            if (!mouseDrag)
                singleClick = true;
            else
                mouseDragEnd = true;
            mouseDrag = false;
            clicked = false;
        }

        if (clicked) {
            if (Vector2.Distance(mousePosition, startingDragPoint) > holdDistance && !mouseDrag)
                mouseDrag = mouseDragStart = true;
        }
    }

    void ResetSingleFrameFlags() {
        if (singleClick)
            singleClick = false;
        if (mouseDragStart)
            mouseDragStart = false;
        if (mouseDragEnd)
            mouseDragEnd = false;
    }

    bool Equal(Vector2 a, Vector2 b) {
        return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y);
    }
}
