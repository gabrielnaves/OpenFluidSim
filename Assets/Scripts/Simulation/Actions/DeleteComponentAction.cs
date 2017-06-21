using UnityEngine;

public class DeleteComponentAction : IAction {

    public GameObject referencedObject;

    public void DoAction() {
        DeactivateConnections(referencedObject.transform.GetChild(0).GetComponent<ComponentConnectors>());
        referencedObject.SetActive(false);
    }

    public void UndoAction() {
        ReactivateConnections(referencedObject.transform.GetChild(0).GetComponent<ComponentConnectors>());
        referencedObject.SetActive(true);
    }

    public void RedoAction() {
        DoAction();
    }

    public void OnDestroy() {}

    private void DeactivateConnections(ComponentConnectors componentConnectors) {
        foreach (var connector in componentConnectors.connectorList)
            RemoveExternalConnections(connector);
    }

    private void RemoveExternalConnections(PneumaticConnector connector) {
        foreach (var other in connector.connectedObjects)
            other.RemoveConnection(connector);
    }

    private void ReactivateConnections(ComponentConnectors componentConnectors) {
        foreach (var connector in componentConnectors.connectorList)
            AddExternalConnections(connector);
    }

    private void AddExternalConnections(PneumaticConnector connector) {
        foreach (var other in connector.connectedObjects)
            other.AddConnection(connector);
    }
}
