using UnityEngine;

/// <summary>
/// Implements component movement (in the editor canvas) functionality
/// </summary>
/// This script implements the possibility to move components (pneumatic and
/// electric) on the editor canvas.
/// It has nothing to do with any component movement during simulation, like
/// valve animations.
/// BUG: It's possible to move components that aren't selected when they overlap
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
        if (RequestedMovement()) { 
            moving = true;
            previousPosition = gameObject.transform.position;
            offset = SimulationInput.instance.mousePosition - (Vector2)gameObject.transform.position;
        }
    }

    bool RequestedMovement() {
        return SimulationInput.instance.mouseButtonDown &&
               componentBox.OverlapPoint(SimulationInput.instance.mousePosition) &&
               SelectedComponent.instance.IsSelected(gameObject);
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
