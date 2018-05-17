using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactSimulation : ElectricComponent {

    public SpriteRenderer activatedMarker;
    [ViewOnly] public Contact.State state;

    Contact contact;
    ComponentReferences connections;
    SpriteRenderer spriteRenderer;
    float receivedSignal = 0;
    bool simulating;

    void Awake() {
        contact = GetComponent<Contact>();
        connections = GetComponent<ComponentReferences>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Setup() {
        contact.correlationTarget.GetComponent<CoilSimulation>().contacts.Add(this);
        state = contact.state;
        simulating = true;
    }

    public override void Stop() {
        simulating = false;
        spriteRenderer.color = Color.black;
        activatedMarker.color = Color.black;
        activatedMarker.gameObject.SetActive(false);
    }

    public override void RespondToSignal(Connector connector, float signal) {
        if (state == Contact.State.closed) {
            receivedSignal = signal;
            foreach (var con in connections.connectorList)
                ElectricSimulationEngine.instance.SpreadSignal(con, signal);
        }
    }

    public void CoilActivated() {
        activatedMarker.gameObject.SetActive(true);
        if (contact.state == Contact.State.closed)
            state = Contact.State.open;
        else if (contact.state == Contact.State.open)
            state = Contact.State.closed;
    }

    public void CoilDeactivated() {
        activatedMarker.gameObject.SetActive(false);
        state = contact.state;
    }

    void LateUpdate() {
        if (simulating) {
            if (receivedSignal > 0) {
                spriteRenderer.color = Color.magenta;
                activatedMarker.color = Color.magenta;
            }
            else if (receivedSignal < 0) {
                spriteRenderer.color = Color.green;
                activatedMarker.color = Color.green;
            }
            else {
                spriteRenderer.color = Color.black;
                activatedMarker.color = Color.black;
            }
            receivedSignal = 0;
        } 
    }
}
