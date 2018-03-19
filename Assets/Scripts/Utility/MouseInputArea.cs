using UnityEngine;

public class MouseInputArea : MonoBehaviour {

    private readonly int LeftMouseButton = 0;
    private readonly int RightMouseButton = 1;

    public bool mouseButton {
        get {
            return Input.GetMouseButton(LeftMouseButton) && GetComponent<Collider2D>().OverlapPoint(mousePosition);
        }
    }

    public bool mouseButtonDown {
        get {
            return Input.GetMouseButtonDown(LeftMouseButton) && GetComponent<Collider2D>().OverlapPoint(mousePosition);
        }
    }

    public bool mouseButtonUp {
        get {
            return Input.GetMouseButtonUp(LeftMouseButton);
        }
    }

    public bool rightMouseButton {
        get {
            return Input.GetMouseButton(RightMouseButton) && GetComponent<Collider2D>().OverlapPoint(mousePosition);
        }
    }

    public bool rightMouseButtonDown {
        get {
            return Input.GetMouseButtonDown(RightMouseButton) && GetComponent<Collider2D>().OverlapPoint(mousePosition);
        }
    }

    public bool rightMouseButtonUp {
        get {
            return Input.GetMouseButtonUp(RightMouseButton);
        }
    }

    public Vector2 mousePosition {
        get {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    /// <summary>
    /// Returns mouse position as world point
    /// </summary>
	public Vector2 GetMousePosition() {
        return mousePosition;
    }

    public bool IsMouseInsideInputArea() {
        return GetComponent<Collider2D>().OverlapPoint(mousePosition);
    }
}
