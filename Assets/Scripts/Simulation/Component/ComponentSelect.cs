﻿using UnityEngine;

public class ComponentSelect : MonoBehaviour {

    public bool isSelected { get; private set; }

    private Collider2D componentBox;

    void Start() {
        componentBox = GetComponent<Collider2D>();
    }

    void Update() {
        UpdateSelectionState();
    }

    private void UpdateSelectionState() {
        if (RequestedSelect()) {
            SelectedComponent.instance.component = gameObject;
            isSelected = true;
            GetComponent<SpriteRenderer>().color = new Color32(191, 186, 255, 255);
        }
        if (isSelected && RequestedDeselect() || isSelected && SelectedComponent.instance.component != gameObject) {
            if (SelectedComponent.instance.component == gameObject)
                SelectedComponent.instance.component = null;
            isSelected = false;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private bool RequestedSelect() {
        return SimulationInput.instance.GetMouseButtonDown() &&
            componentBox.OverlapPoint(SimulationInput.instance.GetMousePosition());
    }

    private bool RequestedDeselect() {
        return SimulationInput.instance.GetMouseButtonDown() &&
            !componentBox.OverlapPoint(SimulationInput.instance.GetMousePosition());
    }
}