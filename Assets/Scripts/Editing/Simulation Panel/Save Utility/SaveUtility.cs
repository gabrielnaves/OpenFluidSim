using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SaveUtility : MonoBehaviour {

    static public SaveUtility instance { get; private set; }

    public string fileName = "savedFile.json";
    public string fileLocation = "SavedFiles/";

    SavedData data;

    void Awake() {
        instance = (SaveUtility)Singleton.Setup(this, instance);
    }

    public void SaveToFile() {
        CreateSavedData();
#if UNITY_WEBGL && !UNITY_EDITOR
        Clipboard.SetClipboard(JsonUtility.ToJson(data, true));
        MessageSystem.instance.GenerateMessage("Saved simulation to clipboard");
#else
        WriteDataToFile();
#endif
    }

    void CreateSavedData() {
        var activeComponents = SimulationPanel.instance.GetActiveComponents();
        data = new SavedData() {
            components = new SavedComponentData[activeComponents.Length]
        };
        for (int i = 0; i < activeComponents.Length; ++i) {
            data.components[i] = new SavedComponentData();
            CreateSavedComponentData(data.components[i], activeComponents[i]);
        }
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

    void WriteDataToFile() {
        StreamWriter file = new StreamWriter(fileLocation + fileName, false, System.Text.Encoding.UTF8);
        file.Write(JsonUtility.ToJson(data, true));
        file.Close();
    }
}
