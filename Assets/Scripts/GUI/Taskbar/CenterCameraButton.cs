using UnityEngine;

public class CenterCameraButton : MonoBehaviour {

	public void CenterCameraPosition() {
        if (SimulationPanel.instance.activeComponents.Count == 0)
            return;

        var components = SimulationPanel.instance.GetActiveComponents();
        Vector3 centroid = Vector3.zero;
        foreach (var component in components)
            centroid += component.transform.position;
        centroid /= components.Length;
        Camera.main.transform.position = new Vector3(centroid.x, centroid.y, Camera.main.transform.position.z);
    }
}
