using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadUtility : MonoBehaviour {

    static public LoadUtility instance { get; private set; }

    public string fileName = "savedFile.json";
    public string fileLocation = "SavedFiles/";

    bool loading = false;
    bool addingToSimulation = false;
    SavedData data;
    Dictionary<int, BaseComponent> idToComponent = new Dictionary<int, BaseComponent>();
    Dictionary<int, Connector> idToConnector;

    void Awake() {
        instance = (LoadUtility)Singleton.Setup(this, instance);
    }

    public void LoadFromFile() {
        Load(clearSimulation: true, loadFromFile: true);
    }

    public void AddFromFile() {
        Load(clearSimulation: false, loadFromFile: true);
    }

    public void LoadFromClipboard() {
        Load(clearSimulation: true, loadFromFile: false);
    }

    public void AddFromClipboard() {
        Load(clearSimulation: false, loadFromFile: false);
    }

    void Load(bool clearSimulation, bool loadFromFile) {
        if (!loading) {
            try {
                ReadDataContainer(loadFromFile);
                VerifyDataIntegrity();
                if (clearSimulation) {
                    ClearCurrentSimulation();
                    addingToSimulation = false;
                }
                else {
                    SelectedObjects.instance.ClearSelection();
                    addingToSimulation = true;
                }
                InstantiateComponents();
            }
            catch (FileNotFoundException) {
                MessageSystem.instance.GenerateMessage("File " + fileLocation + fileName + " could not be found.");
            }
            catch (Exception exception) {
                MessageSystem.instance.GenerateMessage(exception.Message);
            }
        }
    }

    void ReadDataContainer(bool loadFromFile) {
        if (loadFromFile) {
            StreamReader file = new StreamReader(fileLocation + fileName, System.Text.Encoding.UTF8);
            data = JsonUtility.FromJson<SavedData>(file.ReadToEnd());
            file.Close();
        }
        else
            data = JsonUtility.FromJson<SavedData>(Clipboard.GetClipboard());
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
            LoadCoilData(component.coilData, newComponent.GetComponent<Coil>());
        }
        if (addingToSimulation)
            PositionInstantiatedObjectsAroundMousePosition();
        Invoke("CreateConnections", 0.1f);
    }

    void LoadCylinderData(SavedCylinderData savedCylinderData, CylinderEditing cylinderEditing) {
        if (cylinderEditing) {
            cylinderEditing.startingPercentage = savedCylinderData.startingPercentage;
            cylinderEditing.movementTime = savedCylinderData.movementTime;
        }
    }

    void LoadCoilData(SavedCoilData savedCoilData, Coil coil) {
        if (coil) {
            coil.UpdateType((Coil.Type)savedCoilData.coilType);
            coil.UpdateDelay(savedCoilData.delay);
        }
    }

    void PositionInstantiatedObjectsAroundMousePosition() {
        Vector3 mousePosition = SimulationGrid.FitToGrid(EditorInput.instance.mousePosition);
        Vector3 componentCenter = Vector3.zero;
        foreach (var component in idToComponent)
            componentCenter += component.Value.transform.position;
        componentCenter /= idToComponent.Count;
        Vector3 offset = mousePosition - componentCenter;
        foreach (var component in idToComponent)
            component.Value.transform.position = component.Value.transform.position + offset;
    }

    void CreateConnections() {
        BuildConnectorDictionary();
        ConnectConnectors();
        MakeSolenoidCorrelations();
        MakeContactCorrelations();
        if (addingToSimulation) {
            SelectAllNewComponents();
            CreatePasteAction();
        }
        else
            MessageSystem.instance.GenerateMessage("Simulation loaded successfully");
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
                    if (idToConnector.ContainsKey(other)) {
                        Connector otherConnector = idToConnector[other];
                        thisConnector.AddConnection(otherConnector);
                        otherConnector.AddConnection(thisConnector);
                        WireCreator.instance.MakeWire(thisConnector, otherConnector);
                    }
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
                if (component.solenoids[i].configured && idToComponent.ContainsKey(component.solenoids[i].solenoidTargetId)) {
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
                if (idToComponent.ContainsKey(component.contactTargetId)) {
                    Contact contact = idToComponent[component.componentId].GetComponent<Contact>();
                    contact.correlationTarget = idToComponent[component.contactTargetId].GetComponent<CorrelationTarget>();
                    contact.correlationTarget.AddCorrelatedObject(contact);
                }
            }
        }
    }

    void SelectAllNewComponents() {
        foreach (var component in idToComponent)
            SelectedObjects.instance.SelectObject(component.Value);
    }

    void CreatePasteAction() {
        List<BaseComponent> components = new List<BaseComponent>();
        foreach (var component in idToComponent)
            components.Add(component.Value);
        ActionStack.instance.PushAction(new PasteComponentsAction(components));
    }
}
