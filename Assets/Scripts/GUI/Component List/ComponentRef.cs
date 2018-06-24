using UnityEngine;

/// <summary>
/// Used by a Unity Button. Holds the reference to a target floating component,
/// and instantiates it when requested. 
/// </summary>
public class ComponentRef : MonoBehaviour {

    public GameObject floatingComponent;

    /// <summary>
    /// Instantiates the referenced floating component and adds it to the
    /// floating selection
    /// </summary>
    public void SetFloatingComponent() {
        var newFloatingComponent = Instantiate(floatingComponent);
        newFloatingComponent.transform.position =
            SimulationGrid.FitToGrid(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        FloatingSelection.instance.AddComponent(newFloatingComponent);
        ComponentListBar.instance.CloseComponentList();
    }
}
