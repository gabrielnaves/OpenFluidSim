using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Action for deleting components from the simulator editor
/// </summary>
/// Deletion is implemented by disabling the object instead of destroying it.
/// Automatically handles connections to the target component.
public class DeleteObjectsAction : IAction {

    List<BaseComponent> referencedComponents;
    List<Wire> referencedWires;

    public DeleteObjectsAction(List<BaseComponent> componentsToDelete, List<Wire> wiresToDelete) {
        referencedComponents = componentsToDelete;
        referencedWires = wiresToDelete;
        FindAllWiresThatWillBeDeleted();
    }

    public void DoAction() {
        foreach (var wire in referencedWires) {
            SelectedObjects.instance.DeselectObject(wire);
            wire.start.RemoveConnection(wire.end);
            wire.end.RemoveConnection(wire.start);
            wire.gameObject.SetActive(false);
        }
        foreach (var component in referencedComponents) {
            SelectedObjects.instance.DeselectObject(component);
            component.gameObject.SetActive(false);
        }
    }

    public void UndoAction() {
        foreach (var component in referencedComponents)
            component.gameObject.SetActive(true);
        foreach (var wire in referencedWires) {
            wire.start.AddConnection(wire.end);
            wire.end.AddConnection(wire.start);
            wire.gameObject.SetActive(true);
        }
    }

    public void RedoAction() {
        DoAction();
    }

    public void OnDestroy() {}

    public string Name() {
        return "Delete objects";
    }

    void FindAllWiresThatWillBeDeleted() {
        List<Connector> connectorsToDelete = new List<Connector>();
        foreach (var component in referencedComponents)
            foreach (var connector in component.GetComponent<ComponentReferences>().connectorList)
                if (connector.connectedObjects.Count > 0)
                    connectorsToDelete.Add(connector);

        if (connectorsToDelete.Count > 0) {
            foreach (var wire in SimulationPanel.instance.GetActiveWires())
                if (!referencedWires.Contains(wire))
                    if (connectorsToDelete.Contains(wire.start) || connectorsToDelete.Contains(wire.end))
                        referencedWires.Add(wire);
        }
    }
}
