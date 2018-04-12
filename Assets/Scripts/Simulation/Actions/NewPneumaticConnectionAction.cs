using UnityEngine;

/// <summary>
/// Action for connecting two pneumatic connectors
/// </summary>
/// A single wire object is also linked to the connection.
public class NewPneumaticConnectionAction : IAction {

    Wire wire;
    Connector start;
    Connector end;

    public NewPneumaticConnectionAction(Wire wire) {
        this.wire = wire;
        start = wire.start;
        end = wire.end;
    }

    public void DoAction() {
        start.AddConnection(end);
        end.AddConnection(start);
        wire.gameObject.SetActive(true);
    }

    public void UndoAction() {
        start.RemoveConnection(end);
        end.RemoveConnection(start);
        wire.gameObject.SetActive(false);
    }

    public void RedoAction() {
        DoAction();
    }

    public void OnDestroy() {
        Object.Destroy(wire.gameObject);
    }

    public string Name() {
        return "New pneumatic connection";
    }
}
