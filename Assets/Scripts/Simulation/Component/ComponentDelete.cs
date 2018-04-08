using UnityEngine;

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
