using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactSimulation : ElectricComponent {

    public Sprite inactiveSprite;
    public Sprite activeSprite;
    [ViewOnly] public Contact.State state;

    Contact contact;
    ComponentReferences componentReferences;
    SpriteRenderer spriteRenderer;
    float receivedSignal = 0;
    bool simulating;

    void Awake() {
        contact = GetComponent<Contact>();
        componentReferences = GetComponent<ComponentReferences>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Setup() {
        state = contact.state;
        simulating = true;
    }

    public override void Stop() {
        simulating = false;
        spriteRenderer.color = Color.black;
        spriteRenderer.sprite = inactiveSprite;
    }

    public override void RespondToSignal(Connector sourceConnector, float signal) {
        if (state == Contact.State.closed) {
            receivedSignal = signal;
            foreach (var con in componentReferences.connectorList)
                ElectricSimulationEngine.instance.SpreadSignal(con, signal);
        }
    }

    public void SourceActivated() {
        spriteRenderer.sprite = activeSprite;
        if (contact.state == Contact.State.closed)
            state = Contact.State.open;
        else if (contact.state == Contact.State.open)
            state = Contact.State.closed;
    }

    public void SourceDeactivated() {
        spriteRenderer.sprite = inactiveSprite;
        state = contact.state;
    }

    void LateUpdate() {
        if (simulating) {
            if (receivedSignal > 0)
                spriteRenderer.color = Color.magenta;
            else if (receivedSignal < 0)
                spriteRenderer.color = Color.green;
            else
                spriteRenderer.color = Color.black;
            receivedSignal = 0;
        } 
    }
}
