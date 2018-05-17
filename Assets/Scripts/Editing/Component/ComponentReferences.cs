using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds references to connectors, mechanical contacts and solenoids in this component
/// </summary>
public class ComponentReferences : MonoBehaviour {

    [ViewOnly] public List<Connector> connectorList;
    [ViewOnly] public List<MechanicalContact> contactList;
    [ViewOnly] public List<PneumaticSolenoid> solenoidList;

    void Awake() {
        connectorList = new List<Connector>();
        contactList = new List<MechanicalContact>();
        solenoidList = new List<PneumaticSolenoid>();
    }

    public void AddPneumaticConnector(Connector connector) {
        connectorList.Add(connector);
    }

    public void AddMechanicalContact(MechanicalContact contact) {
        contactList.Add(contact);
    }

    public void AddPneumaticSolenoid(PneumaticSolenoid solenoid) {
        solenoidList.Add(solenoid);
    }
}
