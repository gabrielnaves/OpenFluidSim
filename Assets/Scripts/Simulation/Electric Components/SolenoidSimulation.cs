using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolenoidSimulation : ElectricComponent {

    ComponentReferences connections;
    SpriteRenderer spriteRenderer;
    bool simulating;
    bool active;
    bool gotSignal;

    void Awake() {
        connections = GetComponent<ComponentReferences>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Setup() {
        simulating = true;
    }

    public override void Stop() {
        simulating = false;
        spriteRenderer.color = Color.black;
    }

    public override void RespondToSignal(Connector connector, float signal) {
        foreach (var con in connections.connectorList) {
            if (con != connector) {
                if ((con.signal < 0 && signal > 0) || (con.signal > 0 && signal < 0))
                    gotSignal = true;
            }
        }
    }

    void LateUpdate() {
        if (simulating) {
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
    }

    void Deactivate() {
        active = false;
        spriteRenderer.color = Color.black;
    }
}
