using UnityEngine;

public class SimulationInputCameraFollow : MonoBehaviour {

    public Transform mouseInputArea;

    private Vector3 positionOffset;

    void Start() {
        positionOffset = mouseInputArea.position;
    }

    void FixedUpdate() {
        ScaleWithCameraSize();
        FollowCameraPosition();
	}

    private void FollowCameraPosition() {
        mouseInputArea.position = Camera.main.transform.position + positionOffset;
    }

    private void ScaleWithCameraSize() {
        positionOffset = new Vector3(0, Camera.main.orthographicSize * (-0.27f / 5f));
        var cameraSize = Camera.main.orthographicSize;
        mouseInputArea.localScale = new Vector3(cameraSize, cameraSize, cameraSize) * 0.2f;
    }
}
