using UnityEngine;

public class ComponentSelect : MonoBehaviour {

    public bool isSelected { get; private set; }

    private Collider2D componentBox;

    void Start() {
        componentBox = GetComponent<Collider2D>();
    }

    void Update() {
        CheckForSelect();
        CheckForDeselect();
    }

    private void CheckForSelect() {
        if (RequestedSelect()) {
            SelectedComponent.instance.component = gameObject;
            isSelected = true;
            GetComponent<SpriteRenderer>().color = new Color32(191, 186, 255, 255);
        }
    }

    private void CheckForDeselect() {
        if (isSelected && RequestedDeselect() || isSelected && SelectedComponent.instance.component != gameObject) {
            if (SelectedComponent.instance.component == gameObject)
                SelectedComponent.instance.component = null;
            isSelected = false;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private bool RequestedSelect() {
        return SimulationInput.instance.mouseButtonDown &&
            componentBox.OverlapPoint(SimulationInput.instance.mousePosition);
    }

    private bool RequestedDeselect() {
        return SimulationInput.instance.mouseButtonDown &&
            !componentBox.OverlapPoint(SimulationInput.instance.mousePosition);
    }
}
