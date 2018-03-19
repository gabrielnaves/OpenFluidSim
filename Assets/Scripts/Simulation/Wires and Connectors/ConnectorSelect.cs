using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorSelect : MonoBehaviour {

    public GameObject wirePrefab;

    public bool isSelected { get; private set; }

    Collider2D connectorCollider;

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
            if (!HasOtherConnectorSelected())
                SelectThis();
            else
                ConnectConnectors();
        }
    }

    bool RequestedSelect() {
        return SimulationInput.instance.GetMouseButtonDown() &&
               connectorCollider.OverlapPoint(SimulationInput.instance.GetMousePosition()) &&
               !isSelected;
    }

    bool HasOtherConnectorSelected() {
        if (SelectedComponent.instance.HasComponent() && !SelectedComponent.instance.IsSelected(gameObject))
            return SelectedComponent.instance.component.GetComponent<PneumaticConnector>();
        return false;
    }

    void SelectThis() {
        SelectedComponent.instance.component = gameObject;
        isSelected = true;
        WireCreator.instance.StartGeneration(transform.position);
    }

    void ConnectConnectors() {
        var newConnectionAction = new NewPneumaticConnectionAction();
        newConnectionAction.connector1 = GetComponent<PneumaticConnector>();
        newConnectionAction.connector2 = SelectedComponent.instance.component.GetComponent<PneumaticConnector>();
        newConnectionAction.wire = WireCreator.instance.RetrieveWire(transform.position);
        newConnectionAction.DoAction();
        ActionStack.instance.PushAction(newConnectionAction);
        SelectedComponent.instance.component = null;
    }

    void CheckForDeselect() {
        if (isSelected) {
            if (SelectedComponent.instance.component != gameObject) {
                DeselectThis();
            }
            else if (RequestedDeselect()) {
                SelectedComponent.instance.component = null;
                DeselectThis();
            }
        }
    }

    bool RequestedDeselect() {
        return SimulationInput.instance.GetEscapeKeyDown() || SimulationInput.instance.GetRightMouseDown();
            //    || SimulationInput.instance.GetMouseButtonUp();
    }

    void DeselectThis() {
        isSelected = false;
        WireCreator.instance.StopGeneration();
    }
}
