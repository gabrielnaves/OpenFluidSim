using UnityEngine;

/// <summary>
/// Makes the simulation input game object follow the camera position
/// and scale with camera's orthographic size.
/// </summary>
public class SimulationInputCameraFollow : MonoBehaviour {

    /// <summary>
    /// Multiplier to use when converting camera's orthographic size to transform local scale.
    /// </summary>
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
