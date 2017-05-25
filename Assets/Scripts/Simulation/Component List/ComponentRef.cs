using UnityEngine;

public class ComponentRef : MonoBehaviour {

    public GameObject floatingComponent;

    public void SetFloatingComponent() {
        var newFloatingComponent = Instantiate(floatingComponent);
        newFloatingComponent.transform.position =
            SimulationGrid.FitToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        FloatingSelection.instance.AddComponent(newFloatingComponent);
    }
}
