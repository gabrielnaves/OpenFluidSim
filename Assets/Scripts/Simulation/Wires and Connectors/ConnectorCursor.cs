using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorCursor : MonoBehaviour {

    public Texture2D cursorTexture;
    public Vector2 cursorHotspot = new Vector2(8, 8);
    public CursorMode cursorMode = CursorMode.Auto;

    private Collider2D connectorCollider;
    private bool isMouseHovering;

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
