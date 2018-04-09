using UnityEngine;

/// <summary>
/// Updates the cursor image when the mouse is hovering over the attached Collider2D
/// </summary>
/// Used alongside PneumaticConnector. 
[RequireComponent(typeof(Collider2D))]
public class ConnectorCursor : MonoBehaviour {

    public Texture2D cursorTexture;
    public Vector2 cursorHotspot = new Vector2(8, 8);
    public CursorMode cursorMode = CursorMode.Auto;

    Collider2D connectorCollider;
    bool isMouseHovering;

    void Awake() {
        connectorCollider = GetComponent<Collider2D>();
    }

    void Update() {
        if (connectorCollider.OverlapPoint(SimulationInput.instance.mousePosition) && !isMouseHovering) {
            isMouseHovering = true;
            Cursor.SetCursor(cursorTexture, cursorHotspot, cursorMode);
        }
        if (!connectorCollider.OverlapPoint(SimulationInput.instance.mousePosition) && isMouseHovering) {
            isMouseHovering = false;
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}
