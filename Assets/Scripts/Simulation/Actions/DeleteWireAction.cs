using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Action for deleting wires, handling the objects target wire connects.
/// </summary>
/// Deletion is implemented by disabling the object, instead of destroying it.
/// Deleting a wire also disables the corresponding pneumatic connection.
public class DeleteWireAction : IAction {

    Wire referencedWire;

    public DeleteWireAction(Wire referencedWire) {
        this.referencedWire = referencedWire;
    }

    public void DoAction() {
        referencedWire.gameObject.SetActive(false);
        referencedWire.start.RemoveConnection(referencedWire.end);
        referencedWire.end.RemoveConnection(referencedWire.start);
        SimulationPanel.instance.RemoveWire(referencedWire);
    }

    public void UndoAction() {
        referencedWire.gameObject.SetActive(true);
        referencedWire.start.AddConnection(referencedWire.end);
        referencedWire.end.AddConnection(referencedWire.start);
        SimulationPanel.instance.AddWire(referencedWire);
    }

    public void RedoAction() {
        DoAction();
    }

    public void OnDestroy() {}
}
