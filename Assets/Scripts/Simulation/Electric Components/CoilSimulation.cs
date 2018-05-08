using System.Collections.Generic;
using UnityEngine;

public class CoilSimulation : ElectricComponent {

    [ViewOnly] public bool active;
    [ViewOnly] public List<ContactSimulation> contacts = new List<ContactSimulation>();

    ComponentConnections connections;
    SpriteRenderer spriteRenderer;
    bool gotSignal;

    public override void Stop() {
        spriteRenderer.color = Color.white;
        contacts.Clear();
    }

    public override void RespondToSignal(Connector connector, float signal) {
        foreach (var con in connections.connectorList) {
            if (con != connector) {
                if ((con.signal < 0 && signal > 0) || (con.signal > 0 && signal < 0))
                    gotSignal = true;
            }
        }
    }

    void Awake() {
        connections = GetComponent<ComponentConnections>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate() {
        if (SimulationMode.instance.mode == SimulationMode.Mode.simulation) {
            if (!active && gotSignal)
                Activate();
            if (active && !gotSignal)
                Deactivate();
            gotSignal = false;
        }
    }

    void Activate() {
        active = true;
        spriteRenderer.color = Color.magenta;
        foreach (var contact in contacts)
            contact.CoilActivated();
    }

    void Deactivate() {
        active = false;
        spriteRenderer.color = Color.white;
        foreach (var contact in contacts)
            contact.CoilDeactivated();
    }
}
