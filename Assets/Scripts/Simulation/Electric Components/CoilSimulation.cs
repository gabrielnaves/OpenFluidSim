using UnityEngine;

public class CoilSimulation : ElectricComponent {

    [ViewOnly] public bool energized;
    [ViewOnly] public bool active;

    Coil editingCoil;
    ComponentReferences componentReferences;
    SpriteRenderer spriteRenderer;
    bool gotSignal;
    bool simulating;
    float elapsedTime;

    public override void Setup() {
        simulating = true;
    }

    public override void Stop() {
        simulating = false;
        Deenergize();
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
            if (!energized && gotSignal)
                Energize();
            if (energized && !gotSignal)
                Deenergize();
            gotSignal = false;

            elapsedTime += Time.deltaTime;
            if (editingCoil.type == Coil.Type.on_delay && energized && !active) {
                if (elapsedTime > editingCoil.delay)
                    Activate();
            }
            if (editingCoil.type == Coil.Type.off_delay && !energized && active) {
                if (elapsedTime > editingCoil.delay)
                    Deactivate();
            }
        }
    }

    void Energize() {
        energized = true;
        elapsedTime = 0;
        switch (editingCoil.type) {
            case Coil.Type.normal:
            case Coil.Type.off_delay:
                Activate();
                break;
            default:
                break;
        }
    }

    void Deenergize() {
        energized = false;
        elapsedTime = 0;
        switch (editingCoil.type) {
            case Coil.Type.normal:
            case Coil.Type.on_delay:
                Deactivate();
                break;
            default:
                break;
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
