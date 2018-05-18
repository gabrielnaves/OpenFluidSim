using UnityEngine;

public class CoilSimulation : ElectricComponent {

    [ViewOnly] public bool active;

    Coil editingCoil;
    ComponentReferences componentReferences;
    SpriteRenderer spriteRenderer;
    bool gotSignal;
    bool simulating;

    public override void Setup() {
        simulating = true;
    }

    public override void Stop() {
        simulating = false;
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

    void Awake() {
        editingCoil = GetComponent<Coil>();
        componentReferences = GetComponent<ComponentReferences>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        foreach (var contact in editingCoil.correlatedObjects)
            contact.GetComponent<ContactSimulation>().SourceActivated();
    }

    void Deactivate() {
        active = false;
        spriteRenderer.color = Color.white;
        foreach (var contact in editingCoil.correlatedObjects)
            contact.GetComponent<ContactSimulation>().SourceDeactivated();
    }
}
