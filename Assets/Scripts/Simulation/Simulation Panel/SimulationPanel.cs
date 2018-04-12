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
    [ViewOnly] public List<Connector> activePneumaticConnectors;
    [ViewOnly] public List<Connector> activeElectricConnectors;
    [ViewOnly] public List<Wire> activeWires;
    [ViewOnly] public List<Coil> activeCoils;

    List<ISelectable> activeSelectables;
    List<IDraggable> activeDraggables;
    List<IConfigurable> activeConfigurables;

    public BaseComponent[] GetActiveComponents() {
        return activeComponents.ToArray();
    }

    public Connector[] GetActivePneumaticConnectors() {
        return activePneumaticConnectors.ToArray();
    }

    public Connector[] GetActiveElectricConnectors() {
        return activeElectricConnectors.ToArray();
    }

    public Wire[] GetActiveWires() {
        return activeWires.ToArray();
    }

    public Coil[] GetActiveCoils() {
        return activeCoils.ToArray();
    }

    public ISelectable[] GetActiveSelectables() {
        return activeSelectables.ToArray();
    }

    public IDraggable[] GetActiveDraggables() {
        return activeDraggables.ToArray();
    }

    public IConfigurable[] GetActiveConfigurables() {
        return activeConfigurables.ToArray();
    }

    public void AddComponent(BaseComponent component) {
        component.transform.parent = componentsContainer;
        if (!activeComponents.Contains(component))
            activeComponents.Add(component);
    }

    public void RemoveComponent(BaseComponent component) {
        if (activeComponents.Contains(component))
            activeComponents.Remove(component);
    }

    public void AddPneumaticConnector(Connector connector) {
        if (!activePneumaticConnectors.Contains(connector))
            activePneumaticConnectors.Add(connector);
    }

    public void RemovePneumaticConnector(Connector connector) {
        if (activePneumaticConnectors.Contains(connector))
            activePneumaticConnectors.Remove(connector);
    }

    public void AddElectricConnector(Connector connector) {
        if (!activeElectricConnectors.Contains(connector))
            activeElectricConnectors.Add(connector);
    }

    public void RemoveElectricConnector(Connector connector) {
        if (activeElectricConnectors.Contains(connector))
            activeElectricConnectors.Remove(connector);
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

    public void AddCoil(Coil coil) {
        if (!activeCoils.Contains(coil))
            activeCoils.Add(coil);
    }

    public void RemoveCoil(Coil coil) {
        if (activeCoils.Contains(coil))
            activeCoils.Remove(coil);
    }

    public void AddSelectable(ISelectable selectable) {
        if (!activeSelectables.Contains(selectable))
            activeSelectables.Add(selectable);
    }

    public void RemoveSelectable(ISelectable selectable) {
        if (activeSelectables.Contains(selectable))
            activeSelectables.Remove(selectable);
    }

    public void AddDraggable(IDraggable draggable) {
        if (!activeDraggables.Contains(draggable))
            activeDraggables.Add(draggable);
    }

    public void RemoveDraggable(IDraggable draggable) {
        if (activeDraggables.Contains(draggable))
            activeDraggables.Remove(draggable);
    }

    public void AddConfigurable(IConfigurable configurable) {
        if (!activeConfigurables.Contains(configurable))
            activeConfigurables.Add(configurable);
    }

    public void RemoveConfigurable(IConfigurable configurable) {
        if (activeConfigurables.Contains(configurable))
            activeConfigurables.Remove(configurable);
    }

    void Awake() {
        instance = (SimulationPanel)Singleton.Setup(this, instance);
        activeComponents = new List<BaseComponent>();
        activePneumaticConnectors = new List<Connector>();
        activeElectricConnectors = new List<Connector>();
        activeWires = new List<Wire>();
        activeCoils = new List<Coil>();

        activeSelectables = new List<ISelectable>();
        activeDraggables = new List<IDraggable>();
        activeConfigurables = new List<IConfigurable>();
    }
}
