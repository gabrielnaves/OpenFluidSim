using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SaveUtility : MonoBehaviour {

    static public SaveUtility instance { get; private set; }

    public string fileName = "savedFile.json";
    public string fileLocation = "SavedFiles/";

    void Awake() {
        instance = (SaveUtility)Singleton.Setup(this, instance);
    }

    public void SaveSimulationToFile() {
        var data = CreateSavedData(SimulationPanel.instance.GetActiveComponents());
        WriteDataToFile(data);
        MessageSystem.instance.GenerateMessage("Saved simulation to file");
    }

    public string GetSimulationSaveString() {
        var data = CreateSavedData(SimulationPanel.instance.GetActiveComponents());
        return JsonUtility.ToJson(data);
    }

    public string GetSelectedComponentsSaveString() {
        var data = CreateSavedData(SelectedObjects.instance.GetSelectedComponents().ToArray());
        return JsonUtility.ToJson(data);
    }

    SavedData CreateSavedData(BaseComponent[] componentsToSave) {
        var data = new SavedData() {
            components = new SavedComponentData[componentsToSave.Length]
        };
        for (int i = 0; i < componentsToSave.Length; ++i) {
            data.components[i] = new SavedComponentData();
            CreateSavedComponentData(data.components[i], componentsToSave[i]);
        }
        return data;
    }

    void CreateSavedComponentData(SavedComponentData componentData, BaseComponent baseComponent) {
        componentData.name = baseComponent.name;
        componentData.componentId = baseComponent.gameObject.GetInstanceID();
        componentData.position = baseComponent.transform.position;

        ComponentReferences componentReferences = baseComponent.GetComponent<ComponentReferences>();

        List<Connector> connectorList = componentReferences.connectorList;
        componentData.connectors = new SavedConnectorData[connectorList.Count];
        for (int i = 0; i < connectorList.Count; ++i) {
            componentData.connectors[i] = new SavedConnectorData();
            CreateSavedConnectorData(componentData.connectors[i], connectorList[i]);
        }

        List<PneumaticSolenoid> solenoidList = componentReferences.solenoidList;
        componentData.solenoids = new SavedSolenoidData[solenoidList.Count];
        for (int i = 0; i < solenoidList.Count; ++i) {
            componentData.solenoids[i] = new SavedSolenoidData();
            CreateSavedSolenoidData(componentData.solenoids[i], solenoidList[i]);
        }

        FillContactCorrelationInfo(componentData, baseComponent.GetComponent<Contact>());
        FillCylinderInfo(componentData, baseComponent.GetComponent<CylinderEditing>());
        FillCoilInfo(componentData, baseComponent.GetComponent<Coil>());
    }

    void CreateSavedConnectorData(SavedConnectorData connectorData, Connector connector) {
        connectorData.connectorId = connector.gameObject.GetInstanceID();
        connectorData.otherConnectorIds = new int[connector.connectedObjects.Count];
        for (int i = 0; i < connector.connectedObjects.Count; ++i)
            connectorData.otherConnectorIds[i] = connector.connectedObjects[i].gameObject.GetInstanceID();
    }

    void CreateSavedSolenoidData(SavedSolenoidData solenoidData, PneumaticSolenoid solenoid) {
        solenoidData.configured = false;
        solenoidData.solenoidTargetId = 0;
        if ((solenoid as IConfigurable).IsConfigured()) {
            solenoidData.configured = true;
            solenoidData.solenoidTargetId = solenoid.correlationTarget.gameObject.GetInstanceID();
        }
    }

    void FillContactCorrelationInfo(SavedComponentData componentData, Contact contact) {
        componentData.isContact = false;
        componentData.contactTargetId = 0;
        if (contact != null) {
            if ((contact as IConfigurable).IsConfigured()) {
                componentData.isContact = true;
                componentData.contactTargetId = contact.correlationTarget.gameObject.GetInstanceID();
            }
        }
    }

    void FillCylinderInfo(SavedComponentData componentData, CylinderEditing cylinderEditing) {
        if (cylinderEditing) {
            componentData.cylinderData = new SavedCylinderData() {
                startingPercentage = cylinderEditing.startingPercentage,
                movementTime = cylinderEditing.movementTime
            };
        }
    }

    void FillCoilInfo(SavedComponentData componentData, Coil coil) {
        if (coil) {
            componentData.coilData = new SavedCoilData() {
                coilType = (int)coil.type,
                delay = coil.delay
            };
        }
    }

    void WriteDataToFile(SavedData data) {
        StreamWriter file = new StreamWriter(fileLocation + fileName, false, System.Text.Encoding.UTF8);
        file.Write(JsonUtility.ToJson(data, true));
        file.Close();
    }
}
