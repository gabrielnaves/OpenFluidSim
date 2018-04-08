using UnityEngine;

/// <summary>
/// Controls the camera's position and size
/// </summary>
/// TODO: Camera position control is a bit clunky in this implementation
public class CameraControls : MonoBehaviour {

    public float maxCameraSize = 7f;
    public float minCameraSize = 2f;
    public float scrollSpeed = 2f;
    public float offsetScaling = 0.01f;

    private Camera mainCamera;
    private bool movingCamera;
    private Vector3 initialMousePos;
    private Vector3 initialTransformPos;

    void Start() {
        mainCamera = GetComponent<Camera>();
    }

	void Update() {
        UpdateCameraZoom();
        UpdateCameraPosition();
	}

    private void UpdateCameraZoom() {
        float scrollAxis = Input.GetAxis("Mouse ScrollWheel");
        mainCamera.orthographicSize = mainCamera.orthographicSize + scrollAxis * scrollSpeed;
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minCameraSize, maxCameraSize);
    }

    private void UpdateCameraPosition() {
        if (SimulationInput.instance.middleMouseButtonDown) {
            movingCamera = true;
            initialMousePos = SimulationInput.instance.rawMousePosition;
            initialTransformPos = transform.position;
        }
        else if (SimulationInput.instance.middleMouseButtonUp)
            movingCamera = false;
        if (movingCamera) {
            Vector3 offset = SimulationInput.instance.rawMousePosition - initialMousePos;
            transform.position = initialTransformPos - offset * offsetScaling;
        }
    }
}
 