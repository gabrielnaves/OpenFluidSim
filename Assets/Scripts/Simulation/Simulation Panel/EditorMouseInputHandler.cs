using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handler for all mouse inputs on the simulation panel during edit mode
/// </summary>
public class EditorMouseInputHandler : MonoBehaviour {

    bool justPlacedFloatingComponent;
    IDraggable currentDraggable;

    void Update() {
        ProcessMouseInput();
    }

    void ProcessMouseInput() {
        SimulationInput input = SimulationInput.instance;
        SimulationPanel simPanel = SimulationPanel.instance;
        if (input.mouseButtonDown && FloatingSelection.instance.HasFloatingComponent()) {
            FloatingSelection.instance.PlaceFloatingComponent();
            justPlacedFloatingComponent = true;
        }
        if (input.singleClick && !justPlacedFloatingComponent) {
            if (!Input.GetKey(KeyCode.LeftShift))
                SelectedObjects.instance.ClearSelection();
            foreach (var selectable in simPanel.GetActiveSelectables()) {
                if (selectable.RequestedSelect()) {
                    SelectedObjects.instance.SelectObject(selectable);
                    break;
                }
            }
        }
        if (input.mouseDragStart && !justPlacedFloatingComponent) {
            foreach (var draggable in simPanel.GetActiveDraggables()) {
                if (draggable.RequestedDrag()) {
                    draggable.StartDragging();
                    currentDraggable = draggable;
                    break;
                }
            }
            if (currentDraggable == null)
                BoxSelection.instance.StartSelecting();
        }
        if (input.mouseDragEnd) {
            if (currentDraggable != null) {
                currentDraggable.StopDragging();
                currentDraggable = null;
            }
            else
                BoxSelection.instance.StopSelecting();
        }
        if (input.mouseButtonUp)
            justPlacedFloatingComponent = false;
    }
}
