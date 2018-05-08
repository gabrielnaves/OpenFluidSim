using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactSimulation : ElectricComponent {

    [ViewOnly] public Contact.State state;

    Contact contact;
    ComponentConnections connections;

    void Awake() {
        contact = GetComponent<Contact>();
        connections = GetComponent<ComponentConnections>();
    }

    public override void Setup() {
        contact.correlatedContact.GetComponent<CoilSimulation>().contacts.Add(this);
        state = contact.state;
    }

    public override void RespondToSignal(Connector connector, float signal) {
        if (state == Contact.State.closed)
            foreach (var con in connections.connectorList)
                ElectricSimulationEngine.instance.SpreadSignal(con, signal);
    }

    public void CoilActivated() {
        if (contact.state == Contact.State.closed)
            state = Contact.State.open;
        else if (contact.state == Contact.State.open)
            state = Contact.State.closed;
    }

    public void CoilDeactivated() {
        state = contact.state;
    }
}
