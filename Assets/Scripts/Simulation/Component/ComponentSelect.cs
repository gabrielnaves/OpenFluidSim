using UnityEngine;

/// <summary>
/// Implements component selection functionality
/// </summary>
/// Current method for component selection uses two parts: this script and
/// the SelectedComponent class.
/// Each component checks on Update whether it has been selected each frame,
/// and they update the SelectedComponent class accordingly.
/// TODO: Current method does not allow multiple selection, change it
public class ComponentSelect {

    public bool isSelected { get; private set; }

    GameObject gameObject;
    Collider2D componentBox;
    SpriteRenderer spriteRenderer;

    public ComponentSelect(GameObject gameObject, Collider2D componentBox, SpriteRenderer spriteRenderer) {
        this.gameObject = gameObject;
        this.componentBox = componentBox;
        this.spriteRenderer = spriteRenderer;
    }

    public void Update() {
        CheckForSelect();
        CheckForDeselect();
    }

    void CheckForSelect() {
        if (RequestedSelect()) {
            SelectedComponent.instance.component = gameObject;
            isSelected = true;
            spriteRenderer.color = new Color32(191, 186, 255, 255);
        }
    }

    void CheckForDeselect() {
        if (isSelected && RequestedDeselect() || isSelected && SelectedComponent.instance.component != gameObject) {
            if (SelectedComponent.instance.component == gameObject)
                SelectedComponent.instance.component = null;
            isSelected = false;
            spriteRenderer.color = Color.white;
        }
    }

    bool RequestedSelect() {
        return SimulationInput.instance.mouseButtonDown &&
            componentBox.OverlapPoint(SimulationInput.instance.mousePosition);
    }

    bool RequestedDeselect() {
        return SimulationInput.instance.mouseButtonDown &&
            !componentBox.OverlapPoint(SimulationInput.instance.mousePosition);
    }
}
