using UnityEngine;

public class MoveComponentAction : IAction {

    public Vector3 previousPosition;
    public Vector3 newPosition;
    public GameObject referencedObject;

    public void DoAction() {
        referencedObject.transform.position = newPosition;
    }

    public void UndoAction() {
        referencedObject.transform.position = previousPosition;
    }

    public void RedoAction() {
        DoAction();
    }

    public void OnDestroy() {}
}