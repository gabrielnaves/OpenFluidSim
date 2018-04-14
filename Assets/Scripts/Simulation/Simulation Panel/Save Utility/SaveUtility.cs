using System.IO;
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
        WriteDataToFile();
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

        var connections = baseComponent.GetComponent<ComponentConnections>();
        componentData.connectors = new SavedConnectorData[connections.connectorList.Count];
        for (int i = 0; i < connections.connectorList.Count; ++i) {
            componentData.connectors[i] = new SavedConnectorData();
            CreateSavedConnectorData(componentData.connectors[i], connections.connectorList[i]);
        }
    }

    void CreateSavedConnectorData(SavedConnectorData connectorData, Connector connector) {
        connectorData.connectorId = connector.gameObject.GetInstanceID();
        connectorData.otherConnectorIds = new int[connector.connectedObjects.Count];
        for (int i = 0; i < connector.connectedObjects.Count; ++i)
            connectorData.otherConnectorIds[i] = connector.connectedObjects[i].gameObject.GetInstanceID();
    }

    void WriteDataToFile() {
        StreamWriter file = new StreamWriter(fileLocation + fileName, false, System.Text.Encoding.UTF8);
        file.Write(JsonUtility.ToJson(data, true));
        file.Close();
    }
}
