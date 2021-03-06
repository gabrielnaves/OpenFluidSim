﻿using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simulation panel singleton instance
/// </summary>
/// Enables easy referencing to all simulation panel objects.
/// Every simulation object is listed here, in almost every type they can
/// be found in.
/// Objects are expected to add/remove themselves from the simulation panel
/// instance through OnEnable/OnDisable.
public class SimulationPanel : MonoBehaviour {

    static public SimulationPanel instance { get; private set; }

    public Transform componentsContainer;
    public Transform wiresContainer;

    [ViewOnly] public List<BaseComponent> activeComponents;
    [ViewOnly] public List<Connector> activeFluidConnectors;
    [ViewOnly] public List<Connector> activeElectricConnectors;
    [ViewOnly] public List<Wire> activeWires;
    [ViewOnly] public List<Coil> activeCoils;
    [ViewOnly] public List<Sensor> activeSensors;
    [ViewOnly] public List<ElectricSolenoid> activeSolenoids;

    List<ISelectable> activeSelectables;
    List<IDraggable> activeDraggables;
    List<IConfigurable> activeConfigurables;

    public BaseComponent[] GetActiveComponents() {
        return activeComponents.ToArray();
    }

    public BaseComponent[] GetActiveFluidComponents() {
        List<BaseComponent> components = new List<BaseComponent>(activeComponents);
        for (int i = 0; i < components.Count; ++i)
            if (!components[i].CompareTag(Tags.FluidComponent))
                components.RemoveAt(i--);
        return components.ToArray();
    }

    public BaseComponent[] GetActiveElectricComponents() {
        List<BaseComponent> components = new List<BaseComponent>(activeComponents);
        for (int i = 0; i < components.Count; ++i)
            if (!components[i].CompareTag(Tags.ElectricComponent))
                components.RemoveAt(i--);
        return components.ToArray();
    }

    public Connector[] GetActiveFluidConnectors() {
        return activeFluidConnectors.ToArray();
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

    public Sensor[] GetActiveSensors() {
        return activeSensors.ToArray();
    }

    public ElectricSolenoid[] GetActiveSolenoids() {
        return activeSolenoids.ToArray();
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

    public void AddFluidConnector(Connector connector) {
        if (!activeFluidConnectors.Contains(connector))
            activeFluidConnectors.Add(connector);
    }

    public void RemoveFluidConnector(Connector connector) {
        if (activeFluidConnectors.Contains(connector))
            activeFluidConnectors.Remove(connector);
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

    public void AddSensor(Sensor sensor) {
        if (!activeSensors.Contains(sensor))
            activeSensors.Add(sensor);
    }

    public void RemoveSensor(Sensor sensor) {
        if (activeSensors.Contains(sensor))
            activeSensors.Remove(sensor);
    }

    public void AddSolenoid(ElectricSolenoid solenoid) {
        if (!activeSolenoids.Contains(solenoid))
            activeSolenoids.Add(solenoid);
    }

    public void RemoveSolenoid(ElectricSolenoid solenoid) {
        if (activeSolenoids.Contains(solenoid))
            activeSolenoids.Remove(solenoid);
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
        activeFluidConnectors = new List<Connector>();
        activeElectricConnectors = new List<Connector>();
        activeWires = new List<Wire>();
        activeCoils = new List<Coil>();
        activeSensors = new List<Sensor>();
        activeSolenoids = new List<ElectricSolenoid>();

        activeSelectables = new List<ISelectable>();
        activeDraggables = new List<IDraggable>();
        activeConfigurables = new List<IConfigurable>();
    }

    public void ClearEntireSimulation() {
        Camera.main.transform.position = new Vector3(0, 0, Camera.main.transform.position.z);
        SelectedObjects.instance.ClearSelection();
        ActionStack.instance.WipeStacks();
        CreateNewComponentsContainer();
        CreateNewWiresContainer();
    }

    void CreateNewComponentsContainer() {
        Transform newContainer = new GameObject().transform;
        newContainer.parent = componentsContainer.parent;
        newContainer.name = componentsContainer.name;
        Destroy(componentsContainer.gameObject);
        componentsContainer = newContainer;
    }

    void CreateNewWiresContainer() {
        Transform newContainer = new GameObject().transform;
        newContainer.parent = wiresContainer.parent;
        newContainer.name = wiresContainer.name;
        Destroy(wiresContainer.gameObject);
        wiresContainer = newContainer;
    }
}
