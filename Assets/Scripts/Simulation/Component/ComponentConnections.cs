using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds references to pneumatic connections and mechanical
/// contacts in this pneumatic/electric component
/// </summary>
public class ComponentConnections : MonoBehaviour {

    public List<PneumaticConnector> connectorList { get; private set; }
    public List<MechanicalContact> contactList { get; private set; }

    void Awake() {
        connectorList = new List<PneumaticConnector>();
        contactList = new List<MechanicalContact>();
    }

    public void AddPneumaticConnector(PneumaticConnector connector) {
        connectorList.Add(connector);
    }

    public void AddMechanicalContact(MechanicalContact contact) {
        contactList.Add(contact);
    }
}
