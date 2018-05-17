using UnityEngine;

/// <summary>
/// Updates the cursor image when the mouse is hovering over the attached Collider2D
/// </summary>
/// Used alongside Connector. 
[RequireComponent(typeof(Collider2D))]
public class ConnectorCursor : MonoBehaviour {

    public Texture2D cursorTexture;
    public Vector2 cursorHotspot = new Vector2(8, 8);
    public CursorMode cursorMode = CursorMode.Auto;

    new Collider2D collider;
    bool isMouseHovering;

    void Awake() {
        collider = GetComponent<Collider2D>();
    }

    void Update() {
        if (ShouldUpdate())
            UpdateEnabled();
        else
            UpdateDisabled();
    }

    bool ShouldUpdate() {
        return SimulationMode.instance.mode == SimulationMode.Mode.editor &&
               EditorInput.instance.gameObject.activeInHierarchy &&
               !FloatingSelection.instance.HasFloatingComponent();
    }

    void UpdateEnabled() {
        if (collider.OverlapPoint(EditorInput.instance.mousePosition) && !isMouseHovering) {
            isMouseHovering = true;
            Cursor.SetCursor(cursorTexture, cursorHotspot, cursorMode);
        }
        if (!collider.OverlapPoint(EditorInput.instance.mousePosition) && isMouseHovering) {
            isMouseHovering = false;
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }

    void UpdateDisabled() {
        if (isMouseHovering) {
            isMouseHovering = false;
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}
