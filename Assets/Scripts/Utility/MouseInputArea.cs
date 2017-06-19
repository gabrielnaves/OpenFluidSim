using UnityEngine;

public class MouseInputArea : MonoBehaviour {

    public bool mouseButton {
        get {
            return Input.GetMouseButton(0) && GetComponent<Collider2D>().OverlapPoint(mousePosition);
        }
    }

    public bool mouseButtonDown {
        get {
            return Input.GetMouseButtonDown(0) && GetComponent<Collider2D>().OverlapPoint(mousePosition);
        }
    }

    public bool mouseButtonUp {
        get {
            return Input.GetMouseButtonUp(0);
        }
    }

    private Vector2 mousePosition {
        get {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

	public Vector2 GetMousePosition() {
        return mousePosition;
    }
}
