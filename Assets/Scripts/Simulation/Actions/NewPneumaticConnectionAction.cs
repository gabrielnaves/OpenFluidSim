using UnityEngine;

/// <summary>
/// Action for connecting two pneumatic connectors
/// </summary>
/// A single wire object is also linked to the connection.
public class NewPneumaticConnectionAction : IAction {

    PneumaticConnector start;
    PneumaticConnector end;
    GameObject wire;

    public NewPneumaticConnectionAction(PneumaticConnector start, PneumaticConnector end, GameObject wire) {
        this.start = start;
        this.end = end;
        this.wire = wire;
        RedoAction();
    }

    public void UndoAction() {
        start.RemoveConnection(end);
        end.RemoveConnection(start);
        wire.SetActive(false);
    }

    public void RedoAction() {
        start.AddConnection(end);
        end.AddConnection(start);
        wire.SetActive(true);
    }

    ~NewPneumaticConnectionAction() {
        Object.Destroy(wire);
    }
}
