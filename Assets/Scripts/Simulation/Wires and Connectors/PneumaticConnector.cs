using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Implements connections between pneumatic components
/// </summary>
/// This script creates and stores local pneumatic connection information,
/// that is, other components that are connected to this component. 
/// This script also activates the WireCreator when a connection is requested.
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

    void Start() {
        GetComponentInParent<ComponentConnections>().AddPneumaticConnector(this);
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
        return SimulationInput.instance.mouseButtonDown &&
               connectorCollider.OverlapPoint(SimulationInput.instance.mousePosition) &&
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
        if (SimulationInput.instance.mouseButtonUp &&
            connectorCollider.OverlapPoint(SimulationInput.instance.mousePosition))
            return HasOtherConnectorSelected();
        return false;
    }

    bool HasOtherConnectorSelected() {
        if (SelectedComponent.instance.HasComponent() && !SelectedComponent.instance.IsSelected(gameObject))
            return SelectedComponent.instance.component.GetComponent<PneumaticConnector>();
        return false;
    }

    void ConnectConnectors() {
        GameObject wireObj = WireCreator.instance.RetrieveWire(transform.position);
        Wire wire = wireObj.GetComponent<Wire>();
        wire.start = SelectedComponent.instance.component.GetComponent<PneumaticConnector>();
        wire.end = this;
        ActionStack.instance.PushAction(new NewPneumaticConnectionAction(wire.start, wire.end, wireObj));
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
        return isSelected && SimulationInput.instance.mouseButtonUp;
    }
}
