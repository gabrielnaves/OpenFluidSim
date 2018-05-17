using UnityEngine;

public class ElectricSolenoidSimulation : ElectricComponent {

    ElectricSolenoid electricSolenoid;
    ComponentReferences componentReferences;
    SpriteRenderer spriteRenderer;
    bool simulating;
    bool active;
    bool gotSignal;

    void Awake() {
        electricSolenoid = GetComponent<ElectricSolenoid>();
        componentReferences = GetComponent<ComponentReferences>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Setup() {
        simulating = true;
    }

    public override void Stop() {
        simulating = false;
        spriteRenderer.color = Color.black;
        Deactivate();
    }

    public override void RespondToSignal(Connector sourceConnector, float signal) {
        foreach (var connector in componentReferences.connectorList) {
            if (connector != sourceConnector) {
                if ((connector.signal < 0 && signal > 0) || (connector.signal > 0 && signal < 0))
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
        foreach (var solenoid in electricSolenoid.correlatedObjects)
            (solenoid as PneumaticSolenoid).Activate();
    }

    void Deactivate() {
        active = false;
        spriteRenderer.color = Color.black;
        foreach (var solenoid in electricSolenoid.correlatedObjects)
            (solenoid as PneumaticSolenoid).Deactivate();
    }
}
