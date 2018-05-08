using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSimulationEngine : MonoBehaviour {

    public static ElectricSimulationEngine instance { get; private set; }

    [ViewOnly] public Connector[] connectors;
    [ViewOnly] public List<Connector> sourceConnectors = new List<Connector>();
    [ViewOnly] public List<Connector> commonConnectors = new List<Connector>();

    void Awake() {
        instance = (ElectricSimulationEngine)Singleton.Setup(this, instance);
    }

    public void Setup() {
        connectors = SimulationPanel.instance.GetActiveElectricConnectors();
        foreach (var connector in connectors) {
            if (connector.GetComponentInParent<PowerSupplySimulation>())
                sourceConnectors.Add(connector);
            if (connector.GetComponentInParent<ElectricCommonSimulation>())
                commonConnectors.Add(connector);
        }
        foreach (var component in SimulationPanel.instance.GetActiveElectricComponents())
            if (component.GetComponent<ElectricComponent>())
                component.GetComponent<ElectricComponent>().Setup();
    }

    public void Stop() {
        foreach (var component in SimulationPanel.instance.GetActiveElectricComponents())
            if (component.GetComponent<ElectricComponent>())
                component.GetComponent<ElectricComponent>().Stop();
        connectors = null;
        sourceConnectors.Clear();
        commonConnectors.Clear();
    }

    void Update() {
        ClearSignals();
        foreach (var common in commonConnectors)
            SpreadSignal(common, -1f);
        foreach (var source in sourceConnectors)
            SpreadSignal(source, 1f);
    }

    void ClearSignals() {
        if (SimulationMode.instance.mode == SimulationMode.Mode.simulation) {
            foreach (var connector in connectors)
                connector.signal = 0;
        }
    }

    public void SpreadSignal(Connector source, float signal) {
        if (source.signal == 0) {
            source.signal = signal;

            var electricComponent = source.GetComponentInParent<ElectricComponent>();
            if (electricComponent) electricComponent.RespondToSignal(source, signal);

            foreach (var other in source.connectedObjects)
                if (other.signal == 0)
                    SpreadSignal(other, signal);
        }
    }
}
