using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButtonSimulation : ElectricComponent {

    public Contact.State startingState;
    [ViewOnly] public Contact.State state;

    ComponentConnections connections;
    new Collider2D collider;

    void Awake() {
        connections = GetComponent<ComponentConnections>();
        collider = GetComponent<Collider2D>();
    }

    public override void Setup() {
        state = startingState;
    }

    public override void RespondToSignal(Connector source, float signal) {
        if (state == Contact.State.closed)
            foreach (var connector in connections.connectorList)
                ElectricSimulationEngine.instance.SpreadSignal(connector, signal);
    }

    public void Update() {
        if (SimulationMode.instance.mode == SimulationMode.Mode.simulation) {
            if (Input.GetMouseButtonDown(0)) {
                if (collider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                    state = (startingState == Contact.State.open ? Contact.State.closed : Contact.State.open);
            }
            else if (Input.GetMouseButtonUp(0)) {
                state = startingState;
            }
        }
    }
}
