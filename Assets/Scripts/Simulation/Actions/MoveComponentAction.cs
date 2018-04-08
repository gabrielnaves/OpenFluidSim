using UnityEngine;

/// <summary>
/// Action for moving a component on simulation pane
/// </summary>
public class MoveComponentAction : IAction {

    Vector3 previousPosition;
    Vector3 newPosition;
    GameObject referencedObject;

    public MoveComponentAction(GameObject referencedObject, Vector3 previousPosition, Vector3 newPosition) {
        this.referencedObject = referencedObject;
        this.previousPosition = previousPosition;
        this.newPosition = newPosition;
    }

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
