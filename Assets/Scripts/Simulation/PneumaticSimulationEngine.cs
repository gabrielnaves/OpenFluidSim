using System.Collections.Generic;
using UnityEngine;

public class PneumaticSimulationEngine : MonoBehaviour {

    public static PneumaticSimulationEngine instance { get; private set; }

    [ViewOnly] public Connector[] connectors;
    [ViewOnly] public List<Connector> pressureSources = new List<Connector>();

    bool simulating;

    void Awake() {
        instance = (PneumaticSimulationEngine)Singleton.Setup(this, instance);
    }

    public void Setup() {
        connectors = SimulationPanel.instance.GetActivePneumaticConnectors();
        foreach (var connector in connectors) {
            var pressureSource = connector.GetComponentInParent<PressureSourceSimulation>();
            if (pressureSource) pressureSources.Add(connector);
        }
        foreach (var component in SimulationPanel.instance.GetActivePneumaticComponents()) {
            var pneumaticComponent = component.GetComponent<PneumaticComponentSimulation>();
            if (pneumaticComponent) pneumaticComponent.Setup();
        }
        simulating = true;
    }

    public void Stop() {
        ClearSignals();
        foreach (var component in SimulationPanel.instance.GetActivePneumaticComponents()) {
            var pneumaticComponent = component.GetComponent<PneumaticComponentSimulation>();
            if (pneumaticComponent) pneumaticComponent.Stop();
        }
        connectors = null;
        pressureSources.Clear();
        simulating = false;
    }

    void Update() {
        if (simulating) {
            ClearSignals();
            foreach (var source in pressureSources)
                SpreadSignal(source, 1f);
        }
    }

    void ClearSignals() {
        foreach (var connector in connectors)
            connector.signal = 0;
    }

    public void SpreadSignal(Connector source, float signal) {
        if (source.signal == 0) {
            source.signal = signal;

            var pneumaticComponent = source.GetComponentInParent<PneumaticComponentSimulation>();
            if (pneumaticComponent) pneumaticComponent.RespondToSignal(source, signal);

            foreach (var other in source.connectedObjects)
                if (other.signal == 0)
                    SpreadSignal(other, signal);
        }
    }
}
