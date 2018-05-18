using UnityEngine;

public class ProximitySensorSimulation : MonoBehaviour {

    [ViewOnly] public bool active;

    ComponentReferences componentReferences;
    SpriteRenderer spriteRenderer;
    Sensor sensor;

    void Awake() {
        componentReferences = GetComponent<ComponentReferences>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        sensor = GetComponent<Sensor>();
    }

    void LateUpdate() {
        if (SimulationMode.instance.mode == SimulationMode.Mode.simulation) {
            if (componentReferences.contactList[0].IsTouching() && !active)
                Activate();
            else if (!componentReferences.contactList[0].IsTouching() && active)
                Deactivate();
        }
        else if (active) {
            Deactivate();
        }
    }

    void Activate() {
        active = true;
        spriteRenderer.color = Color.green;
        foreach (var contact in sensor.correlatedObjects)
            contact.GetComponent<ContactSimulation>().SourceActivated();
    }

    void Deactivate() {
        active = false;
        spriteRenderer.color = Color.white;
        foreach (var contact in sensor.correlatedObjects)
            contact.GetComponent<ContactSimulation>().SourceDeactivated();
    }
}
