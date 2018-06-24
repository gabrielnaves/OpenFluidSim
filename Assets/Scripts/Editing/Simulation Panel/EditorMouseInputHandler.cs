using UnityEngine;

/// <summary>
/// Handler for all mouse inputs on the simulation panel during edit mode
/// </summary>
public class EditorMouseInputHandler : MonoBehaviour {

    IDraggable currentDraggable;

    void Update() {
        ProcessMouseInput();
    }

    void ProcessMouseInput() {
        EditorInput input = EditorInput.instance;
        SimulationPanel simPanel = SimulationPanel.instance;
        if (FloatingSelection.instance.HasFloatingComponent()) {
            if (input.singleClick)
                FloatingSelection.instance.PlaceFloatingComponent();
        }
        else {
            if (input.singleClick) {
                if (!Input.GetKey(KeyCode.LeftShift))
                    SelectedObjects.instance.ClearSelection();
                foreach (var selectable in simPanel.GetActiveSelectables()) {
                    if (selectable.RequestedSelect()) {
                        SelectedObjects.instance.SelectObject(selectable);
                        break;
                    }
                }
            }
            if (input.doubleClick) {
                foreach (var configurable in simPanel.GetActiveConfigurables()) {
                    if (configurable.RequestedConfig()) {
                        SelectedObjects.instance.ClearSelection();
                        configurable.OpenConfigWindow();
                        break;
                    }
                }
            }
            if (input.mouseDragStart) {
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
        }
    }
}
