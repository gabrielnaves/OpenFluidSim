using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds references to pneumatic connections and mechanical
/// contacts in this pneumatic/electric component
/// </summary>
public class ComponentConnections : MonoBehaviour {

    [ViewOnly] public List<Connector> connectorList;
    [ViewOnly] public List<MechanicalContact> contactList;

    void Awake() {
        connectorList = new List<Connector>();
        contactList = new List<MechanicalContact>();
    }

    public void AddPneumaticConnector(Connector connector) {
        connectorList.Add(connector);
    }

    public void AddMechanicalContact(MechanicalContact contact) {
        contactList.Add(contact);
    }
}
