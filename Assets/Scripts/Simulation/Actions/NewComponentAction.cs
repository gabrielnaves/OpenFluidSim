using UnityEngine;

/// <summary>
/// Action for creating a new component on the simulation pane
/// </summary>
/// The newly-instantiated game object is then added to SimulationPane.
public class NewComponentAction : IAction {

    public Vector3 componentPosition;
    public GameObject componentPrefab;

    private GameObject componentCreated;

    public void DoAction() {
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

    public void OnDestroy() {
        Object.Destroy(componentCreated);
    }
}
