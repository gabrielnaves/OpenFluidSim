using UnityEngine;

public class DeleteComponentAction : IAction {

    public GameObject referencedObject;

    public void DoAction() {
        referencedObject.SetActive(false);
        // Any other code relative to component deletion must go here
    }

    public void UndoAction() {
        referencedObject.SetActive(true);
    }

    public void RedoAction() {
        DoAction();
    }

    public void OnDestroy() {}
}
