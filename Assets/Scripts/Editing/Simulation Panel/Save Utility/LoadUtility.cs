using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadUtility : MonoBehaviour {

    static public LoadUtility instance { get; private set; }

    public string fileName = "savedFile.json";
    public string fileLocation = "SavedFiles/";

    bool loading = false;
    SavedData data;
    Dictionary<int, BaseComponent> idToComponent = new Dictionary<int, BaseComponent>();
    Dictionary<int, Connector> idToConnector;

    void Awake() {
        instance = (LoadUtility)Singleton.Setup(this, instance);
    }

    public void LoadFromFile() {
        if (!loading) {
            try {
                ReadDataContainer();
                VerifyDataIntegrity();
                ClearCurrentSimulation();
                InstantiateComponents();
#if UNITY_WEBGL && !UNITY_EDITOR
                MessageSystem.instance.GenerateMessage("Loaded simulation from clipboard");
#endif
            }
            catch (FileNotFoundException) {
                MessageSystem.instance.GenerateMessage("File " + fileLocation+fileName + " could not be found.");
            }
            catch (Exception exception) {
                MessageSystem.instance.GenerateMessage(exception.Message);
            }
        }
    }

    void ReadDataContainer() {
#if UNITY_WEBGL && !UNITY_EDITOR
        data = JsonUtility.FromJson<SavedData>(Clipboard.GetClipboard());
#else
        StreamReader file = new StreamReader(fileLocation + fileName, System.Text.Encoding.UTF8);
        data = JsonUtility.FromJson<SavedData>(file.ReadToEnd());
        file.Close();
#endif
    }

    void VerifyDataIntegrity() {
        if (data == null)
            throw new Exception("Failed to read any data.");
        if (data.components.Length == 0)
            throw new Exception("Could not read any component data.");
    }

    void ClearCurrentSimulation() {
        SimulationPanel.instance.ClearEntireSimulation();
    }

    void InstantiateComponents() {
        loading = true;
        idToComponent = new Dictionary<int, BaseComponent>();
        foreach (var component in data.components) {
            GameObject newComponent = Instantiate(ComponentLibrary.instance.nameToPrefab[component.name]);
            newComponent.transform.position = component.position;
            newComponent.name = newComponent.name.Replace("(Clone)", "");
            idToComponent[component.componentId]= newComponent.GetComponent<BaseComponent>();
            LoadCylinderData(component.cylinderData, newComponent.GetComponent<CylinderEditing>());
        }
        Invoke("CreateConnections", 0.1f);
    }

    void LoadCylinderData(SavedCylinderData savedCylinderData, CylinderEditing cylinderEditing) {
        if (cylinderEditing) {
            cylinderEditing.startingPercentage = savedCylinderData.startingPercentage;
            cylinderEditing.movementTime = savedCylinderData.movementTime;
        }
    }

    void CreateConnections() {
        BuildConnectorDictionary();
        ConnectConnectors();
        MakeSolenoidCorrelations();
        MakeContactCorrelations();
        loading = false;
    }

    void BuildConnectorDictionary() {
        idToConnector = new Dictionary<int, Connector>();
        foreach (var component in data.components) {
            for (int i = 0; i < component.connectors.Length; ++i) {
                BaseComponent target = idToComponent[component.componentId];
                ComponentReferences targetConnections = target.GetComponent<ComponentReferences>();
                idToConnector[component.connectors[i].connectorId] = targetConnections.connectorList[i];
            }
        }
    }

    void ConnectConnectors() {
        foreach (var component in data.components) {
            foreach (var connector in component.connectors) {
                Connector thisConnector = idToConnector[connector.connectorId];
                foreach (var other in connector.otherConnectorIds) {
                    Connector otherConnector = idToConnector[other];
                    thisConnector.AddConnection(otherConnector);
                    otherConnector.AddConnection(thisConnector);
                    WireCreator.instance.MakeWire(thisConnector, otherConnector);
                }
            }
        }
    }

    void MakeSolenoidCorrelations() {
        foreach (var component in data.components) {
            if (component.solenoids.Length == 0)
                continue;
            var componentReferences = idToComponent[component.componentId].GetComponent<ComponentReferences>();
            for (int i = 0; i < component.solenoids.Length; ++i) {
                if (component.solenoids[i].configured) {
                    componentReferences.solenoidList[i].correlationTarget =
                        idToComponent[component.solenoids[i].solenoidTargetId].GetComponent<CorrelationTarget>();
                    componentReferences.solenoidList[i].correlationTarget.AddCorrelatedObject(componentReferences.solenoidList[i]);
                }
            }
        }
    }

    void MakeContactCorrelations() {
        foreach (var component in data.components) {
            if (component.isContact) {
                Contact contact = idToComponent[component.componentId].GetComponent<Contact>();
                contact.correlationTarget = idToComponent[component.contactTargetId].GetComponent<CorrelationTarget>();
                contact.correlationTarget.AddCorrelatedObject(contact);
            }
        }
    }
}
