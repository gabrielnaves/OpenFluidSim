using UnityEngine;

/// <summary>
/// Implements component deletion functionality
/// </summary>
/// Component deletion is actually "soft" deletion, as operations
/// can be undone. As such, deleting an object only deactivates it.
/// Component objects on scene are only "hard" deleted when their
/// corresponding NewComponentAction is destroyed.
public class ComponentDelete {

    GameObject gameObject;

    public ComponentDelete(GameObject gameObject) {
        this.gameObject = gameObject;
    }

	public void Update() {
        if (RequestedDelete())
            CreateDeleteAction();
	}

    bool RequestedDelete() {
        return SelectedComponent.instance.component == gameObject && Input.GetKeyDown(KeyCode.Delete);
    }

    void CreateDeleteAction() {
        ActionStack.instance.PushAction(new DeleteComponentAction(gameObject));
    }
}
