using UnityEngine;

public class MouseInputArea : MonoBehaviour {

    public bool mouseButton { get; private set; }
    public bool mouseButtonDown { get; private set; }
    public bool mouseButtonUp { get; private set; }
    private Vector2 clickedPoint = Vector2.zero;

    /// <summary>
	/// Gets the click position. Returns Vector2.zero on frames with no input.
	/// </summary>
	public Vector2 GetClickPosition() {
        return (mouseButton ? clickedPoint : Vector2.zero);
    }
    
	void LateUpdate() {
        bool lastMouseButton = mouseButton;
        UpdateClickedPoint();
        UpdateMouseButton();
        UpdateMouseButtonDown(lastMouseButton);
        UpdateMouseButtonUp(lastMouseButton);
    }

    private void UpdateClickedPoint() {
        clickedPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void UpdateMouseButton() {
        if (Input.GetMouseButtonDown(0))
            mouseButton = GetComponent<Collider2D>().OverlapPoint(clickedPoint);
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
