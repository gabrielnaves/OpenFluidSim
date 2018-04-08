using UnityEngine;

/// <summary>
/// Component for Handling mouse inputs within an area represented as a Collider2D
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class MouseInputArea : MonoBehaviour {

    const int LeftMouseButton = 0;
    const int RightMouseButton = 1;
    const int MiddleMouseButton = 2;

    new protected Collider2D collider;

    void Awake() {
        collider = GetComponent<Collider2D>();
    }

    public bool mouseButton {
        get {
            return Input.GetMouseButton(LeftMouseButton) && IsMouseInsideInputArea();
        }
    }

    public bool mouseButtonDown {
        get {
            return Input.GetMouseButtonDown(LeftMouseButton) && IsMouseInsideInputArea();
        }
    }

    public bool mouseButtonUp {
        get {
            return Input.GetMouseButtonUp(LeftMouseButton) || !IsMouseInsideInputArea();
        }
    }

    public bool rightMouseButton {
        get {
            return Input.GetMouseButton(RightMouseButton) && IsMouseInsideInputArea();
        }
    }

    public bool rightMouseButtonDown {
        get {
            return Input.GetMouseButtonDown(RightMouseButton) && IsMouseInsideInputArea();
        }
    }

    public bool rightMouseButtonUp {
        get {
            return Input.GetMouseButtonUp(RightMouseButton) || !IsMouseInsideInputArea();
        }
    }

    public bool middleMouseButton {
        get {
            return Input.GetMouseButton(MiddleMouseButton) && IsMouseInsideInputArea();
        }
    }

    public bool middleMouseButtonDown {
        get {
            return Input.GetMouseButtonDown(MiddleMouseButton) && IsMouseInsideInputArea();
        }
    }

    public bool middleMouseButtonUp {
        get {
            return Input.GetMouseButtonUp(MiddleMouseButton) || !IsMouseInsideInputArea();
        }
    }

    /// <summary>
    /// Mouse position as world point
    /// </summary>
    public Vector2 mousePosition {
        get {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    public Vector3 rawMousePosition {
        get {
            return Input.mousePosition;
        }
    }

    public bool IsMouseInsideInputArea() {
        return collider.OverlapPoint(mousePosition);
    }
}
