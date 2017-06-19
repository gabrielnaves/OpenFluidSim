using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorSelect : MonoBehaviour {

    public bool isSelected { get; private set; }

    private Collider2D connectorCollider;

    void Start() {
        connectorCollider = GetComponent<Collider2D>();
    }

    void Update() {
        CheckForSelect();
    }

    void LateUpdate() {
        CheckForDeselect();
    }

    void CheckForSelect() {
        if (RequestedSelect()) {
            if (HasOtherConnectorSelected())
                ConnectConnectors();
            else
                SelectThisConnector();
        }
    }

    void CheckForDeselect() {
        if (isSelected && RequestedDeselect() || isSelected && SelectedComponent.instance.component != gameObject) {
            if (SelectedComponent.instance.component == gameObject)
                SelectedComponent.instance.component = null;
            isSelected = false;
        }
    }

    private bool RequestedSelect() {
        return SimulationInput.instance.GetMouseButtonDown() &&
            connectorCollider.OverlapPoint(SimulationInput.instance.GetMousePosition());
    }

    private bool RequestedDeselect() {
        return SimulationInput.instance.GetMouseButtonDown() &&
            !connectorCollider.OverlapPoint(SimulationInput.instance.GetMousePosition());
    }

    private bool HasOtherConnectorSelected() {
        if (SelectedComponent.instance.component != null)
            if (SelectedComponent.instance.component != gameObject)
                return SelectedComponent.instance.component.GetComponent<PneumaticConnector>();
        return false;
    }

    private void ConnectConnectors() {
        var thisConnector = GetComponent<PneumaticConnector>();
        var otherConnector = SelectedComponent.instance.component.GetComponent<PneumaticConnector>();
        thisConnector.AddConnection(otherConnector);
        otherConnector.AddConnection(thisConnector);
    }

    private void SelectThisConnector() {
        SelectedComponent.instance.component = gameObject;
        isSelected = true;
    }
}
