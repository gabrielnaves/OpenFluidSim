using UnityEngine;

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
        if (Input.GetMouseButtonDown(2)/* && SimulationInput.instance.IsMouseOnInputArea()*/) {
            movingCamera = true;
            initialMousePos = Input.mousePosition;
            initialTransformPos = transform.position;
        }
        else if (Input.GetMouseButtonUp(2))
            movingCamera = false;
        if (movingCamera) {
            Vector3 offset = Input.mousePosition - initialMousePos;
            transform.position = initialTransformPos - offset * offsetScaling;
        }
    }
}
