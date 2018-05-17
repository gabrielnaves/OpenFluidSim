using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Implements component movement (in the editor canvas) functionality
/// </summary>
/// This script implements the possibility to move components (pneumatic and
/// electric) on the editor canvas.
/// It has nothing to do with any component movement during simulation, like
/// valve animations.
public class ComponentMove {

    public bool moving = false;
    List<BaseComponent> selectedComponents = new List<BaseComponent>();
    List<Vector2> initialPositions = new List<Vector2>();

    public void StartMoving() {
        moving = true;
        foreach (var component in SimulationPanel.instance.activeComponents)
            if (SelectedObjects.instance.IsSelected(component)) {
                selectedComponents.Add(component);
                initialPositions.Add(component.transform.position);
            }
    }

    public void StopMoving() {
        CreateMovementAction();
        selectedComponents.Clear();
        initialPositions.Clear();
        moving = false;
    }

    public void Update() {
        if (moving)
            MoveSelectedComponentsWithMouse();
    }

    void MoveSelectedComponentsWithMouse() {
        Vector2 offset = EditorInput.instance.mousePosition - EditorInput.instance.startingDragPoint;
        for (int i = 0; i < selectedComponents.Count; ++i)
            selectedComponents[i].transform.position = SimulationGrid.FitToGrid(initialPositions[i] + offset);
    }

    void CreateMovementAction() {
        ActionStack.instance.PushAction(new MoveComponentAction(
            selectedComponents.ToArray(), initialPositions.ToArray(), CreateNewPositionsArray()
        ));
    }

    Vector2[] CreateNewPositionsArray() {
        List<Vector2> newPositions = new List<Vector2>();
        foreach (var component in selectedComponents)
            newPositions.Add(component.transform.position);
        return newPositions.ToArray();
    }
}
