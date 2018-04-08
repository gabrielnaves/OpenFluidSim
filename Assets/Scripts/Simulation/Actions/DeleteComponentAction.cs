using UnityEngine;

/// <summary>
/// Action for deleting components from the simulator editor
/// </summary>
/// Deletion is implemented by disabling the object instead of destroying it.
/// Automatically handles connections to the target component.
public class DeleteComponentAction : IAction {

    GameObject referencedObject;

    public DeleteComponentAction(GameObject referencedObject) {
        this.referencedObject = referencedObject;
        RedoAction();
    }

    public void UndoAction() {
        ReactivateConnections(referencedObject.transform.GetChild(0).GetComponent<ComponentConnectors>());
        referencedObject.SetActive(true);
    }

    public void RedoAction() {
        DeactivateConnections(referencedObject.transform.GetChild(0).GetComponent<ComponentConnectors>());
        referencedObject.SetActive(false);
    }
    
    void DeactivateConnections(ComponentConnectors componentConnectors) {
        if (componentConnectors != null)
            foreach (var connector in componentConnectors.connectorList)
                RemoveExternalConnections(connector);
    }

    void RemoveExternalConnections(PneumaticConnector connector) {
        foreach (var other in connector.connectedObjects)
            other.RemoveConnection(connector);
    }

    void ReactivateConnections(ComponentConnectors componentConnectors) {
        if (componentConnectors != null)
            foreach (var connector in componentConnectors.connectorList)
                AddExternalConnections(connector);
    }

    void AddExternalConnections(PneumaticConnector connector) {
        foreach (var other in connector.connectedObjects)
            other.AddConnection(connector);
    }
}
