using System.Collections.Generic;
using UnityEngine;

public class PneumaticConnector : MonoBehaviour {

    public List<PneumaticConnector> connectedObjects = new List<PneumaticConnector>();

    Color openColor = Color.red;
    Color selectedColor = Color.green;
    Color connectedColor = Color.clear;

    Collider2D connectorCollider;
    SpriteRenderer spriteRenderer;

    bool isSelected;

    void Awake() {
        connectorCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = openColor;
    }

    public void AddConnection(PneumaticConnector other) {
        if (!connectedObjects.Contains(other))
            connectedObjects.Add(other);
    }

    public void RemoveConnection(PneumaticConnector other) {
        if (connectedObjects.Contains(other))
            connectedObjects.Remove(other);
    }

    void UpdateColor() {
        if (isSelected)
            spriteRenderer.color = selectedColor;
        else if (connectedObjects.Count > 0)
            spriteRenderer.color = connectedColor;
        else
            spriteRenderer.color = openColor;
    }

    void Update() {
        CheckForSelect();
        CheckForConnect();
        UpdateColor();
    }

    void CheckForSelect() {
        if (RequestedSelect())
            SelectThis();
    }

    bool RequestedSelect() {
        return SimulationInput.instance.GetMouseButtonDown() &&
               connectorCollider.OverlapPoint(SimulationInput.instance.GetMousePosition()) &&
               !isSelected;
    }

    void SelectThis() {
        isSelected = true;
        SelectedComponent.instance.component = gameObject;
        WireCreator.instance.StartGeneration(transform.position);
    }

    void CheckForConnect() {
        if (RequestedConnect())
            ConnectConnectors();
    }

    bool RequestedConnect() {
        if (SimulationInput.instance.GetMouseButtonUp() &&
            connectorCollider.OverlapPoint(SimulationInput.instance.GetMousePosition()))
            return HasOtherConnectorSelected();
        return false;
    }

    bool HasOtherConnectorSelected() {
        if (SelectedComponent.instance.HasComponent() && !SelectedComponent.instance.IsSelected(gameObject))
            return SelectedComponent.instance.component.GetComponent<PneumaticConnector>();
        return false;
    }

    void ConnectConnectors() {
        var newConnectionAction = new NewPneumaticConnectionAction();
        newConnectionAction.start = SelectedComponent.instance.component.GetComponent<PneumaticConnector>();
        newConnectionAction.end = this;
        newConnectionAction.wire = WireCreator.instance.RetrieveWire(transform.position);
        newConnectionAction.wire.GetComponent<Wire>().start = newConnectionAction.start;
        newConnectionAction.wire.GetComponent<Wire>().end = newConnectionAction.end;
        ActionStack.instance.PushAction(newConnectionAction);
    }

    void LateUpdate() {
        CheckForDeselect();
    }

    void CheckForDeselect() {
        if (RequestedDeselect()) {
            SelectedComponent.instance.component = null;
            isSelected = false;
            WireCreator.instance.StopGeneration();
        }
    }

    bool RequestedDeselect() {
        return isSelected && SimulationInput.instance.GetMouseButtonUp();
    }
}
