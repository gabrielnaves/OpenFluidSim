using UnityEngine;

public class CenterCameraButton : TaskbarButton {

	public void CenterCameraPosition() {
        var components = SimulationPanel.instance.GetActiveComponents();
        Vector3 centroid = Vector3.zero;
        foreach (var component in components)
            centroid += component.transform.position;
        centroid /= components.Length;
        Camera.main.transform.position = new Vector3(centroid.x, centroid.y, Camera.main.transform.position.z);
    }

    protected override bool ShouldShowButton() {
        return SimulationPanel.instance.activeComponents.Count > 0;
    }
}
