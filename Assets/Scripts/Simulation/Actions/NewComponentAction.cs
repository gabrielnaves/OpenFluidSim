using UnityEngine;

public class NewComponentAction : IAction {

    public Transform componentPosition;
    public GameObject componentCreated;

    public void OnDestroy() {
        Object.Destroy(componentCreated);
    }

    public void RedoAction() {
        componentCreated.SetActive(true);
    }

    public void UndoAction() {
        componentCreated.SetActive(false);
    }
}
