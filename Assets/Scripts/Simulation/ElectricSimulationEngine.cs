using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSimulationEngine : MonoBehaviour {

    public static ElectricSimulationEngine instance { get; private set; }

    [ViewOnly] public Connector[] connectors;
    [ViewOnly] public List<Connector> sourceConnectors;

    void Awake() {
        instance = (ElectricSimulationEngine)Singleton.Setup(this, instance);
    }

    public void Setup() {
        connectors = SimulationPanel.instance.GetActiveElectricConnectors();
        foreach (var connector in connectors) {
            if (connector.transform.parent.parent.name.Contains("Power Supply"))
                sourceConnectors.Add(connector);
        }
    }

    void Update() {
        ClearSignals();
        foreach (var source in sourceConnectors)
            SpreadSignal(source);
    }

    void ClearSignals() {
        if (SimulationMode.instance.mode == SimulationMode.Mode.simulation) {
            foreach (var connector in connectors)
                connector.signal = false;
        }
    }

    void SpreadSignal(Connector source) {
        if (source.signal != true) {
            source.signal = true;
            foreach (var other in source.connectedObjects)
                if (other.signal == false)
                    SpreadSignal(other);
        }
    }
}
