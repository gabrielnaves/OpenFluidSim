using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simulation panel singleton instance
/// </summary>
/// Enables easy referencing to simulation panel object, and provides
/// a function for adding new components (pneumatic, etc.) to the
/// components container.
public class SimulationPanel : MonoBehaviour {

    static public SimulationPanel instance { get; private set; }

    public Transform componentsContainer;
    public Transform wiresContainer;

    [ViewOnly] public List<BaseComponent> activeComponents;
    [ViewOnly] public List<Wire> activeWires;

    List<ISelectable> activeSelectables;
    List<IDraggable> activeDraggables;

    public BaseComponent[] GetActiveComponents() {
        return activeComponents.ToArray();
    }

    public Wire[] GetActiveWires() {
        return activeWires.ToArray();
    }

    public ISelectable[] GetActiveSelectables() {
        return activeSelectables.ToArray();
    }

    public IDraggable[] GetActiveDraggables() {
        return activeDraggables.ToArray();
    }

    public void AddComponent(BaseComponent component) {
        component.transform.parent = componentsContainer;
        if (!activeComponents.Contains(component)) {
            activeComponents.Add(component);
            AddSelectablesAndDraggables(component.gameObject);
        }
    }

    public void RemoveComponent(BaseComponent component) {
        if (activeComponents.Contains(component)) {
            activeComponents.Remove(component);
            RemoveSelectablesAndDraggables(component.gameObject);
        }
    }

    public void AddWire(Wire wire) {
        wire.transform.parent = wiresContainer;
        if (!activeWires.Contains(wire)) {
            activeWires.Add(wire);
            AddSelectablesAndDraggables(wire.gameObject);
        }
    }

    public void RemoveWire(Wire wire) {
        if (activeWires.Contains(wire)) {
            activeWires.Remove(wire);
            RemoveSelectablesAndDraggables(wire.gameObject);
        }
    }

    void AddSelectablesAndDraggables(GameObject obj) {
        foreach (var selectable in obj.GetComponentsInChildren<ISelectable>())
            activeSelectables.Add(selectable);
        foreach (var draggable in obj.GetComponentsInChildren<IDraggable>())
            activeDraggables.Add(draggable);
    }

    void RemoveSelectablesAndDraggables(GameObject obj) {
        foreach (var selectable in obj.GetComponentsInChildren<ISelectable>())
            activeSelectables.Remove(selectable);
        foreach (var draggable in obj.GetComponentsInChildren<IDraggable>())
            activeDraggables.Remove(draggable);
    }

    void Awake() {
        instance = (SimulationPanel)Singleton.Setup(this, instance);
        activeSelectables = new List<ISelectable>();
        activeDraggables = new List<IDraggable>();
    }
}
