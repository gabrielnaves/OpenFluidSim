﻿using UnityEngine;

/// <summary>
/// Action for deleting components from the simulator editor
/// </summary>
/// Deletion is implemented by disabling the object instead of destroying it.
/// Automatically handles connections to the target component.
public class DeleteComponentAction : IAction {

    GameObject referencedObject;

    public DeleteComponentAction(GameObject referencedObject) {
        this.referencedObject = referencedObject;
    }

    public void DoAction() {
        DeactivateConnections(referencedObject.GetComponent<ComponentConnections>());
        referencedObject.SetActive(false);
    }

    public void UndoAction() {
        ReactivateConnections(referencedObject.GetComponent<ComponentConnections>());
        referencedObject.SetActive(true);
    }

    public void RedoAction() {
        DoAction();
    }

    public void OnDestroy() {}
    
    void DeactivateConnections(ComponentConnections componentConnectors) {
        if (componentConnectors != null)
            foreach (var connector in componentConnectors.connectorList)
                RemoveExternalConnections(connector);
    }

    void RemoveExternalConnections(PneumaticConnector connector) {
        foreach (var other in connector.connectedObjects)
            other.RemoveConnection(connector);
    }

    void ReactivateConnections(ComponentConnections componentConnectors) {
        if (componentConnectors != null)
            foreach (var connector in componentConnectors.connectorList)
                AddExternalConnections(connector);
    }

    void AddExternalConnections(PneumaticConnector connector) {
        foreach (var other in connector.connectedObjects)
            other.AddConnection(connector);
    }
}
