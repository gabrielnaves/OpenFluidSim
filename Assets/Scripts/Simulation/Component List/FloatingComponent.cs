using UnityEngine;

/// <summary>
/// A component that will be added to simulation pane, but hasn't been positioned yet
/// </summary>
/// The floating component will follow the mouse position until the user clicks
/// on a valid position inside the editor canvas, or cancels the operation.
/// When a valid position for the component is chosen, the floating component
/// will instantiate a NewComponentAction and add it to the ActionStack, and
/// will remove itself from the floating selection. 
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
        ActionStack.instance.PushAction(new NewComponentAction(componentPrefab, transform.position));
    }
}
