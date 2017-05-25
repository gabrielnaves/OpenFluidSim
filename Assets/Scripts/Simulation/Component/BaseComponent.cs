using UnityEngine;

public class BaseComponent : MonoBehaviour {

    private bool moving = false;
    private Vector3 previousPosition;
    private Vector2 offset;

    void Update() {
        CheckForClick();
        CheckForRelease();
        if (moving)
            FollowMouse();
    }

    private void CheckForClick() {
        if (SimulationInput.instance.GetMouseButtonDown())
            if (GetComponent<Collider2D>().OverlapPoint(SimulationInput.instance.GetMousePosition())) {
                moving = true;
                previousPosition = transform.position;
                offset = SimulationInput.instance.GetMousePosition() - (Vector2)transform.position;
            }
    }

    private void CheckForRelease() {
        if (moving && SimulationInput.instance.GetMouseButtonUp()) {
            moving = false;
            if (!Equals(previousPosition, transform.position))
                MakeMovementAction();
        }
    }

    private void MakeMovementAction() {
        var newAction = new MoveComponentAction();
        newAction.previousPosition = previousPosition;
        newAction.newPosition = transform.position;
        newAction.referencedObject = gameObject;
        ActionStack.instance.PushAction(newAction);
    }

    private void FollowMouse() {
        var mousePos = SimulationInput.instance.GetMousePosition();
        transform.position = SimulationGrid.FitToGrid(mousePos - offset);
    }
}
