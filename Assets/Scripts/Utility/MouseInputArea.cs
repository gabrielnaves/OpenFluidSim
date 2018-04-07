using UnityEngine;

/// <summary>
/// Component for Handling mouse inputs within an area represented as a Collider2D
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class MouseInputArea : MonoBehaviour {

    const int LeftMouseButton = 0;
    const int RightMouseButton = 1;

    new Collider2D collider;

    void Awake() {
        collider = GetComponent<Collider2D>();
    }

    public bool mouseButton {
        get {
            return Input.GetMouseButton(LeftMouseButton) && collider.OverlapPoint(mousePosition);
        }
    }

    public bool mouseButtonDown {
        get {
            return Input.GetMouseButtonDown(LeftMouseButton) && collider.OverlapPoint(mousePosition);
        }
    }

    public bool mouseButtonUp {
        get {
            return Input.GetMouseButtonUp(LeftMouseButton);
        }
    }

    public bool rightMouseButton {
        get {
            return Input.GetMouseButton(RightMouseButton) && collider.OverlapPoint(mousePosition);
        }
    }

    public bool rightMouseButtonDown {
        get {
            return Input.GetMouseButtonDown(RightMouseButton) && collider.OverlapPoint(mousePosition);
        }
    }

    public bool rightMouseButtonUp {
        get {
            return Input.GetMouseButtonUp(RightMouseButton);
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

    public bool IsMouseInsideInputArea() {
        return collider.OverlapPoint(mousePosition);
    }
}
