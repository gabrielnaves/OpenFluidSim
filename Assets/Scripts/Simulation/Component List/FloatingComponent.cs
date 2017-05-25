using UnityEngine;

public class FloatingComponent : MonoBehaviour {

    public GameObject componentPrefab;

	void Update () {
        FollowMousePosition();
        if (Input.GetMouseButtonDown(0)) {
            CreateObjectOnSimulationPane();
            FloatingSelection.instance.RemoveCurrentComponent();
        }
    }

    private void FollowMousePosition() {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = SimulationGrid.FitToGrid(mousePosition);
        transform.position = mousePosition;
    }

    private void CreateObjectOnSimulationPane() {
        var action = new NewComponentAction();
        action.componentPosition = transform.position;
        action.componentPrefab = componentPrefab;
        action.DoAction();
        ActionStack.instance.PushAction(action);
    }
}
