using System.IO;
using UnityEngine;

public class SaveUtility : MonoBehaviour {

    static public SaveUtility instance { get; private set; }

    public string fileName = "savedFile.json";
    public string fileLocation = "SavedFiles/";

    BaseComponentData data;

    void Awake() {
        instance = (SaveUtility)Singleton.Setup(this, instance);
    }

    public void SaveToFile() {
        CreateComponentDataContainer();
        WriteDataToFile();
    }

    void CreateComponentDataContainer() {
        var activeComponents = SimulationPanel.instance.GetActiveComponents();
        data = new BaseComponentData() {
            components = new string[activeComponents.Length],
            positions = new Vector3[activeComponents.Length],
            componentIds = new int[activeComponents.Length],
            connections = new ComponentConnectionsData[activeComponents.Length]
        };
        for (int i = 0; i < activeComponents.Length; ++i) {
            data.components[i] = activeComponents[i].name;
            data.positions[i] = activeComponents[i].transform.position;
            data.componentIds[i] = activeComponents[i].gameObject.GetInstanceID();
            data.connections[i] = new ComponentConnectionsData();
            CreateConnectionsDataContainer(data.connections[i], activeComponents[i]);
        }
    }

    void CreateConnectionsDataContainer(ComponentConnectionsData connectionData, BaseComponent baseComponent) {
        var componentConnections = baseComponent.GetComponent<ComponentConnections>();
        connectionData.thisComponentId = baseComponent.gameObject.GetInstanceID();
        connectionData.connectorIds = new int[componentConnections.connectorList.Count];
        connectionData.connectors = new ConnectorData[componentConnections.connectorList.Count];
        for (int i = 0; i < componentConnections.connectorList.Count; ++i) {
            connectionData.connectorIds[i] = componentConnections.connectorList[i].gameObject.GetInstanceID();
            connectionData.connectors[i] = new ConnectorData();
            CreateConnectorDataContainer(connectionData.connectors[i], connectionData,
                componentConnections.connectorList[i]);
        }
    }

    void CreateConnectorDataContainer(ConnectorData connectorData, ComponentConnectionsData parentConnector, Connector connector) {
        connectorData.parentConnectionId = parentConnector.thisComponentId;
        connectorData.thisConnectorId = connector.gameObject.GetInstanceID();
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
