using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handler for all mouse inputs on the simulation panel during edit mode
/// </summary>
public class EditorMouseInputHandler : MonoBehaviour {

    bool justPlacedFloatingComponent;

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
            SelectedObjects.instance.ClearSelection();
            foreach (var selectable in simPanel.GetActiveSelectables()) {
                if (selectable.RequestedSelect()) {
                    SelectedObjects.instance.SelectComponent(selectable);
                    break;
                }
            }
        }
        if (input.mouseDragStart && !justPlacedFloatingComponent) {
            BoxSelection.instance.StartSelecting();
        }
        if (input.mouseDragEnd) {
            BoxSelection.instance.StopSelecting();
        }
        if (input.mouseButtonUp)
            justPlacedFloatingComponent = false;
    }
}
