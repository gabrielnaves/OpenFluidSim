using UnityEngine;

/// <summary>
/// Controls the camera's position and size
/// </summary>
public class CameraControls : MonoBehaviour {

    public float maxCameraSize = 7f;
    public float minCameraSize = 2f;
    public float scrollSpeed = 2f;
    public float offsetScaling = 0.01f;

    private Camera mainCamera;
    private Transform cameraTransform;
    private bool movingCamera;
    private Vector3 initialMousePos;
    private Vector3 initialTransformPos;

    void Start() {
        mainCamera = Camera.main;
        cameraTransform = mainCamera.transform;
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
        if (EditorInput.instance.rightMouseButtonDown) {
            movingCamera = true;
            initialMousePos = EditorInput.instance.rawMousePosition;
            initialTransformPos = cameraTransform.position;
        }
        else if (EditorInput.instance.rightMouseButtonUp)
            movingCamera = false;
        if (movingCamera) {
            Vector3 offset = EditorInput.instance.rawMousePosition - initialMousePos;
            cameraTransform.position = initialTransformPos - offset * offsetScaling;
        }
    }

    public void UpdateCameraZoom(float value) {
        mainCamera.orthographicSize = Mathf.Clamp(value, minCameraSize, maxCameraSize);
    }
}
 