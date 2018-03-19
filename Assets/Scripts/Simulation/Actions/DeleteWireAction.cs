using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteWireAction : IAction {

    public Wire referencedWire;

    public void DoAction() {
        referencedWire.gameObject.SetActive(false);
        referencedWire.start.RemoveConnection(referencedWire.end);
        referencedWire.end.RemoveConnection(referencedWire.start);
    }

    public void UndoAction() {
        referencedWire.gameObject.SetActive(true);
        referencedWire.start.AddConnection(referencedWire.end);
        referencedWire.end.AddConnection(referencedWire.start);
    }

    public void RedoAction() {
        DoAction();
    }

    public void OnDestroy() {}
}
