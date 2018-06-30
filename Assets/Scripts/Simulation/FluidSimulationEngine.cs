using System.Collections.Generic;
using UnityEngine;

public class FluidSimulationEngine : MonoBehaviour {

    public static FluidSimulationEngine instance { get; private set; }

    [ViewOnly] public Connector[] connectors;
    [ViewOnly] public List<Connector> pressureSources = new List<Connector>();
    [ViewOnly] public List<Connector> exhausts = new List<Connector>();
    [ViewOnly] public List<Connector> reservoirs = new List<Connector>();

    bool simulating;

    void Awake() {
        instance = (FluidSimulationEngine)Singleton.Setup(this, instance);
    }

    public void Setup() {
        connectors = SimulationPanel.instance.GetActiveFluidConnectors();
        foreach (var connector in connectors) {
            var pressureSource = connector.GetComponentInParent<PressureSourceSimulation>();
            if (pressureSource) pressureSources.Add(connector);
            var exhaust = connector.GetComponentInParent<ExhaustSimulation>();
            if (exhaust) exhausts.Add(connector);
            var reservoir = connector.GetComponentInParent<ReservoirSimulation>();
            if (reservoir) reservoirs.Add(connector);
        }
        foreach (var component in SimulationPanel.instance.GetActiveFluidComponents()) {
            var fluidComponent = component.GetComponent<FluidComponentSimulation>();
            if (fluidComponent) fluidComponent.Setup();
        }
        simulating = true;
    }

    public void Stop() {
        ClearSignals();
        foreach (var component in SimulationPanel.instance.GetActiveFluidComponents()) {
            var fluidComponent = component.GetComponent<FluidComponentSimulation>();
            if (fluidComponent) fluidComponent.Stop();
        }
        connectors = null;
        pressureSources.Clear();
        exhausts.Clear();
        reservoirs.Clear();
        simulating = false;
    }

    void Update() {
        if (simulating) {
            ClearSignals();
            foreach (var source in pressureSources)
                SpreadSignal(source, 1f);
            foreach (var exhaust in exhausts)
                SpreadSignal(exhaust, -1f);
            foreach (var reservoir in reservoirs)
                SpreadSignal(reservoir, -1f);
        }
    }

    void ClearSignals() {
        foreach (var connector in connectors)
            connector.signal = 0;
    }

    public void SpreadSignal(Connector source, float signal) {
        if (source.signal == 0) {
            source.signal = signal;

            var fluidComponent = source.GetComponentInParent<FluidComponentSimulation>();
            if (fluidComponent) fluidComponent.RespondToSignal(source, signal);

            foreach (var other in source.connectedObjects)
                if (other.signal == 0)
                    SpreadSignal(other, signal);
        }
    }
}
