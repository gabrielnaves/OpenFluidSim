using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Action for deleting wires, handling the objects target wire connects.
/// </summary>
/// Deletion is implemented by disabling the object, instead of destroying it.
public class DeleteWireAction : IAction {

    Wire referencedWire;

    public DeleteWireAction(Wire referencedWire) {
        this.referencedWire = referencedWire;
        RedoAction();
    }

    public void UndoAction() {
        referencedWire.gameObject.SetActive(true);
        referencedWire.start.AddConnection(referencedWire.end);
        referencedWire.end.AddConnection(referencedWire.start);
    }

    public void RedoAction() {
        referencedWire.gameObject.SetActive(false);
        referencedWire.start.RemoveConnection(referencedWire.end);
        referencedWire.end.RemoveConnection(referencedWire.start);
    }
}
