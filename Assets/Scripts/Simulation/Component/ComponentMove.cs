using UnityEngine;

public class ComponentMove {

    bool moving = false;
    Vector3 previousPosition;
    Vector2 offset;

    GameObject gameObject;
    Collider2D componentBox;

    public ComponentMove(GameObject gameObject, Collider2D componentBox) {
        this.gameObject = gameObject;
        this.componentBox = componentBox;
    }

    public void Update() {
        CheckForClick();
        CheckForRelease();
        if (moving)
            FollowMouse();
    }

    void CheckForClick() {
        if (SimulationInput.instance.mouseButtonDown)
            if (componentBox.OverlapPoint(SimulationInput.instance.mousePosition)) {
                moving = true;
                previousPosition = gameObject.transform.position;
                offset = SimulationInput.instance.mousePosition - (Vector2)gameObject.transform.position;
            }
    }

    void CheckForRelease() {
        if (moving && SimulationInput.instance.mouseButtonUp) {
            moving = false;
            if (!Equals(previousPosition, gameObject.transform.position))
                MakeMovementAction();
        }
    }

    void MakeMovementAction() {
        var newAction = new MoveComponentAction();
        newAction.previousPosition = previousPosition;
        newAction.newPosition = gameObject.transform.position;
        newAction.referencedObject = gameObject;
        ActionStack.instance.PushAction(newAction);
    }

    void FollowMouse() {
        var mousePos = SimulationInput.instance.mousePosition;
        gameObject.transform.position = SimulationGrid.FitToGrid(mousePos - offset);
    }
}
