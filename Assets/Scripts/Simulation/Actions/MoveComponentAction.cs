using UnityEngine;

/// <summary>
/// Action for moving a component on simulation pane
/// </summary>
public class MoveComponentAction : IAction {

    GameObject referencedObject;
    Vector3 previousPosition;
    Vector3 newPosition;

    public MoveComponentAction(GameObject referencedObject, Vector3 previousPosition, Vector3 newPosition) {
        this.referencedObject = referencedObject;
        this.previousPosition = previousPosition;
        this.newPosition = newPosition;
        RedoAction();
    }

    public void UndoAction() {
        referencedObject.transform.position = previousPosition;
    }

    public void RedoAction() {
        referencedObject.transform.position = newPosition;
    }
}
