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

    [ViewOnly]
    public List<BaseComponent> activeComponents;

    [ViewOnly]
    public List<Wire> activeWires;

    public void AddComponent(BaseComponent component) {
        component.transform.parent = componentsContainer;
        if (!activeComponents.Contains(component))
            activeComponents.Add(component);
    }

    public void RemoveComponent(BaseComponent component) {
        if (activeComponents.Contains(component))
            activeComponents.Remove(component);
    }

    public void AddWire(Wire wire) {
        wire.transform.parent = wiresContainer;
        if (!activeWires.Contains(wire))
            activeWires.Add(wire);
    }

    public void RemoveWire(Wire wire) {
        if (activeWires.Contains(wire))
            activeWires.Remove(wire);
    }

    void Awake() {
        instance = (SimulationPanel)Singleton.Setup(this, instance);
    }
}
