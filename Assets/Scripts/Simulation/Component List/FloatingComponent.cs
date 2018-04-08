using UnityEngine;

/// <summary>
/// The component when it hasn't yet been added to simulation pane
/// </summary>
/// This script represents the second step to adding a new component to the
/// simulation pane. The floating component is 
public class FloatingComponent : MonoBehaviour {

    public GameObject componentPrefab;

	void Update () {
        FollowMousePosition();
        CheckForEscapeInput();
        CheckForMouseInput();
    }

    private void FollowMousePosition() {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = SimulationGrid.FitToGrid(mousePosition);
        transform.position = mousePosition;
    }

    private void CheckForEscapeInput() {
        if (Input.GetKeyDown(KeyCode.Escape))
            FloatingSelection.instance.RemoveCurrentComponent();
    }

    private void CheckForMouseInput() {
        if (SimulationInput.instance.mouseButtonDown) {
            CreateObjectOnSimulationPane();
            FloatingSelection.instance.RemoveCurrentComponent();
        }
    }

    private void CreateObjectOnSimulationPane() {
        ActionStack.instance.PushAction(new NewComponentAction(transform.position, componentPrefab));
    }
}
