using UnityEngine;

/// <summary>
/// Action for creating a new component on the simulation pane
/// </summary>
/// The newly-instantiated game object is then added to the SimulationPane.
public class NewComponentAction : IAction {

    GameObject componentPrefab;
    Vector3 componentPosition;

    GameObject createdComponent;

    public NewComponentAction(GameObject componentPrefab, Vector3 componentPosition) {
        this.componentPrefab = componentPrefab;
        this.componentPosition = componentPosition;
    }

    public void DoAction() {
        createdComponent = Object.Instantiate(componentPrefab);
        createdComponent.transform.position = componentPosition;
        SimulationPane.instance.AddNewObject(createdComponent);
    }

    public void UndoAction() {
        createdComponent.SetActive(false);
    }

    public void RedoAction() {
        createdComponent.SetActive(true);
    }

    public void OnDestroy() {
        Object.Destroy(createdComponent);
    }
}
