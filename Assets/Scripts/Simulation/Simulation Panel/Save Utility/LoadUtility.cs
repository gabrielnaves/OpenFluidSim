using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadUtility : MonoBehaviour {

    static public LoadUtility instance { get; private set; }

    public string fileName = "savedFile.json";
    public string fileLocation = "SavedFiles/";

    bool loading = false;
    BaseComponentData data;
    Dictionary<int, BaseComponent> idToComponent = new Dictionary<int, BaseComponent>();
    Dictionary<int, Connector> idToConnector;

    void Awake() {
        instance = (LoadUtility)Singleton.Setup(this, instance);
    }

    public void LoadFromFile() {
        if (!loading) {
            try {
                ReadDataContainer();
                ClearCurrentSimulation();
                InstantiateComponents();
            }
            catch (FileNotFoundException) {
                Debug.LogError("File " + fileLocation+fileName + " could not be found.");
            }
        }
    }

    void ReadDataContainer() {
        StreamReader file = new StreamReader(fileLocation + fileName, System.Text.Encoding.UTF8);
        data = JsonUtility.FromJson<BaseComponentData>(file.ReadToEnd());
        file.Close();
    }

    void ClearCurrentSimulation() {
        SimulationPanel.instance.ClearEntireSimulation();
    }

    void InstantiateComponents() {
        loading = true;
        idToComponent = new Dictionary<int, BaseComponent>();
        for (int i = 0; i < data.components.Length; ++i) {
            GameObject newComponent = Instantiate(ComponentLibrary.instance.nameToPrefab[data.components[i]]);
            newComponent.transform.position = data.positions[i];
            newComponent.name = newComponent.name.Replace("(Clone)", "");
            idToComponent[data.componentIds[i]] = newComponent.GetComponent<BaseComponent>();
        }
        Invoke("CreateConnections", 0.1f);
    }

    void CreateConnections() {
        BuildConnectorDictionary();
        ConnectConnectors();
        loading = false;
    }

    void BuildConnectorDictionary() {
        idToConnector = new Dictionary<int, Connector>();
        foreach (var connection in data.connections) {
            for (int i = 0; i < connection.connectors.Length; ++i) {
                BaseComponent component = idToComponent[connection.thisComponentId];
                ComponentConnections componentConnections = component.GetComponent<ComponentConnections>();
                idToConnector[connection.connectors[i].thisConnectorId] = componentConnections.connectorList[i];
            }
        }
    }

    void ConnectConnectors() {
        foreach (var connection in data.connections) {
            foreach (var connector in connection.connectors) {
                Connector thisConnector = idToConnector[connector.thisConnectorId];
                foreach (var other in connector.otherConnectorIds) {
                    Connector otherConnector = idToConnector[other];
                    thisConnector.AddConnection(otherConnector);
                    otherConnector.AddConnection(thisConnector);
                    WireCreator.instance.MakeWire(thisConnector, otherConnector);
                }
            }
        }
    }
}
