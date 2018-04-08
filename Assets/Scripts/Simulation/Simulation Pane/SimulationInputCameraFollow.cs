using UnityEngine;

public class SimulationInputCameraFollow : MonoBehaviour {

    public float camSizeToScale = 0.33f;

    void FixedUpdate() {
        ScaleWithCameraSize();
        FollowCameraPosition();
	}

    private void FollowCameraPosition() {
        transform.position = Camera.main.transform.position;
    }

    private void ScaleWithCameraSize() {
        var cameraSize = Camera.main.orthographicSize;
        transform.localScale = new Vector3(cameraSize, cameraSize, cameraSize) * camSizeToScale;
    }
}
