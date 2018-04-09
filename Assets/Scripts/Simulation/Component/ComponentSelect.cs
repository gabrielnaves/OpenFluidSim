using UnityEngine;

public class ComponentSelect {

    public bool isSelected { get; private set; }

    GameObject gameObject;
    Collider2D[] colliders;

    public ComponentSelect() {
        gameObject = null;
        colliders = new Collider2D[0];
    }

    public ComponentSelect(GameObject gameObject, Collider2D collider) {
        this.gameObject = gameObject;
        colliders = new Collider2D[] { collider };
    }

    public ComponentSelect(GameObject gameObject, Collider2D[] colliders) {
        this.gameObject = gameObject;
        this.colliders = colliders;
    }

    public void Update() {
        if (InvalidReferences()) {
            isSelected = false;
            return;
        }

        if (MultiSelection.instance.isSelecting) {
            isSelected = false;
            foreach (var collider in colliders) {
                if (MultiSelection.instance.TouchingSelectionBox(collider))
                    isSelected = true;
            }
        }
        else if (SimulationInput.instance.mouseButtonDown) {
            isSelected = false;
            foreach (var collider in colliders) {
                if (collider.OverlapPoint(SimulationInput.instance.mousePosition))
                    isSelected = true;
            }
        }
        if (SimulationInput.instance.GetEscapeKeyDown())
            isSelected = false;
        if (isSelected)
            SelectedComponents.instance.SelectComponent(gameObject);
        else
            SelectedComponents.instance.DeselectComponent(gameObject);
    }

    bool InvalidReferences() {
        foreach (var collider in colliders)
            if (collider == null) return true;
        return gameObject == null;
    }
}
