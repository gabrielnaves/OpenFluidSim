using UnityEngine;

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
