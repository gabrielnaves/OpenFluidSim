using UnityEngine;

/// <summary>
/// Action for creating a new component on the simulation pane
/// </summary>
public class NewComponentAction : IAction {

    GameObject componentPrefab;
    Vector3 componentPosition;

    BaseComponent createdComponent;

    public NewComponentAction(GameObject componentPrefab, Vector3 componentPosition) {
        this.componentPrefab = componentPrefab;
        this.componentPosition = componentPosition;
    }

    public void DoAction() {
        createdComponent = Object.Instantiate(componentPrefab).GetComponent<BaseComponent>();
        createdComponent.transform.position = componentPosition;
        createdComponent.name = createdComponent.name.Replace("(Clone)", "");
    }

    public void UndoAction() {
        createdComponent.gameObject.SetActive(false);
        SelectedObjects.instance.DeselectObject(createdComponent);
    }

    public void RedoAction() {
        createdComponent.gameObject.SetActive(true);
    }

    public void OnDestroy() {
        Object.Destroy(createdComponent.gameObject);
    }

    public string Name() {
        return "New component: " + createdComponent.name;
    }
}
