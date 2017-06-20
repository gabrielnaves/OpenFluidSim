using UnityEngine;

public class CameraControls : MonoBehaviour {

    public float maxCameraSize = 7f;
    public float minCameraSize = 2f;
    public float scrollSpeed = 2f;

    private Camera mainCamera;

    void Start() {
        mainCamera = GetComponent<Camera>();
    }

	void Update() {
        UpdateCameraZoom();
	}

    private void UpdateCameraZoom() {
        float scrollAxis = Input.GetAxis("Mouse ScrollWheel");
        mainCamera.orthographicSize = mainCamera.orthographicSize + scrollAxis * scrollSpeed;
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minCameraSize, maxCameraSize);
    }
}
