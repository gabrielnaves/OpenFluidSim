using UnityEngine;

/// <summary>
/// Action for creating a new component on the simulation pane
/// </summary>
/// The newly-instantiated game object is then added to SimulationPane.
public class NewComponentAction : IAction {

    GameObject componentCreated;

    public NewComponentAction(Vector3 componentPosition, GameObject componentPrefab) {
        componentCreated = Object.Instantiate(componentPrefab);
        componentCreated.transform.position = componentPosition;
        SimulationPane.instance.AddNewObject(componentCreated);
    }

    public void UndoAction() {
        componentCreated.SetActive(false);
    }

    public void RedoAction() {
        componentCreated.SetActive(true);
    }

    ~NewComponentAction() {
        Object.Destroy(componentCreated);
    }
}
