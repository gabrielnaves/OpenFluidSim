using UnityEngine;

public class MouseInputArea : MonoBehaviour {

    public bool mouseButton { get; private set; }
    public bool mouseButtonDown { get; private set; }
    public bool mouseButtonUp { get; private set; }
    private Vector2 mousePosition = Vector2.zero;

	public Vector2 GetMousePosition() {
        return mousePosition;
    }
    
	void LateUpdate() {
        bool lastMouseButton = mouseButton;
        UpdateMousePosition();
        UpdateMouseButton();
        UpdateMouseButtonDown(lastMouseButton);
        UpdateMouseButtonUp(lastMouseButton);
    }

    private void UpdateMousePosition() {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void UpdateMouseButton() {
        if (Input.GetMouseButton(0))
            mouseButton = GetComponent<Collider2D>().OverlapPoint(mousePosition);
        else
            mouseButton = false;
    }

    private void UpdateMouseButtonDown(bool lastMouseButton) {
        if (mouseButton == true && lastMouseButton == false)
            mouseButtonDown = true;
        else
            mouseButtonDown = false;
    }

    private void UpdateMouseButtonUp(bool lastMouseButton) {
        if (mouseButton == false && lastMouseButton == true)
            mouseButtonUp = true;
        else
            mouseButtonUp = false;
    }
}
