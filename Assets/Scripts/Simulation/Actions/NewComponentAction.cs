using UnityEngine;

/// <summary>
/// Action for creating a new component on the simulation pane
/// </summary>
/// The newly-instantiated game object is then added to the SimulationPane.
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
        SimulationPanel.instance.AddComponent(createdComponent);
    }

    public void UndoAction() {
        createdComponent.gameObject.SetActive(false);
        SimulationPanel.instance.RemoveComponent(createdComponent);
    }

    public void RedoAction() {
        createdComponent.gameObject.SetActive(true);
        SimulationPanel.instance.AddComponent(createdComponent);
    }

    public void OnDestroy() {
        Object.Destroy(createdComponent);
    }
}
