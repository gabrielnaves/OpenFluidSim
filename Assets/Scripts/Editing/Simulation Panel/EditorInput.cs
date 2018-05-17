using UnityEngine;

/// <summary>
/// Produces input flags specific to the simulation panel
/// </summary>
/// This class extends MouseInputArea's functionality via inheritance.
/// Unlike the base class, this one is inteded to be unique on scene,
/// and is a singleton.
public class EditorInput : MouseInputArea {

    static public EditorInput instance { get; private set; }

    public float holdDistance = 0.1f;
    public float doubleClickTime = 0.2f;

    public bool singleClick { get; private set; }
    public bool doubleClick { get; private set; }
    public bool mouseDrag { get; private set; }
    public bool mouseDragStart { get; private set; }
    public bool mouseDragEnd { get; private set; }
    public Vector2 startingDragPoint { get; private set; }

    void Awake() {
        instance = (EditorInput)Singleton.Setup(this, instance);
        collider = GetComponent<Collider2D>();
    }

    bool clicked = false;
    bool waitingSecondClick = false;
    float elapsedTime = 0;

    void Update() {
        ResetSingleFrameFlags();
        UpdateDoubleClickTimer();

        if (mouseButtonDown) {
            startingDragPoint = mousePosition;
            clicked = true;
        }

        if (mouseButtonUp && clicked) {
            if (!mouseDrag) {
                if (!waitingSecondClick) {
                    waitingSecondClick = true;
                    singleClick = true;
                }
                else {
                    waitingSecondClick = false;
                    elapsedTime = 0f;
                    doubleClick = true;
                }
            }
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
        if (doubleClick)
            doubleClick = false;
        if (mouseDragStart)
            mouseDragStart = false;
        if (mouseDragEnd)
            mouseDragEnd = false;
    }

    void UpdateDoubleClickTimer() {
        if (waitingSecondClick) {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > doubleClickTime) {
                waitingSecondClick = false;
                elapsedTime = 0f;
            }
        }
    }

    bool Equal(Vector2 a, Vector2 b) {
        return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y);
    }
}
