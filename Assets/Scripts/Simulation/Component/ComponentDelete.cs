using UnityEngine;

public class ComponentDelete : MonoBehaviour {

	void Update () {
        if (RequestedDelete())
            CreateDeleteAction();
	}

    private bool RequestedDelete() {
        return SelectedComponent.instance.component == gameObject && Input.GetKeyDown(KeyCode.Delete);
    }

    private void CreateDeleteAction() {
        var deleteAction = new DeleteComponentAction();
        deleteAction.referencedObject = gameObject;
        ActionStack.instance.PushAction(deleteAction);
    }
}
