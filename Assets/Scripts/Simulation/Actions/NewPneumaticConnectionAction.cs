using UnityEngine;

/// <summary>
/// Action for connecting two pneumatic connectors
/// </summary>
/// A single wire object is also linked to the connection.
public class NewPneumaticConnectionAction : IAction {

    public PneumaticConnector start;
    public PneumaticConnector end;
    public GameObject wire;

    public void DoAction() {
        start.AddConnection(end);
        end.AddConnection(start);
        wire.SetActive(true);
    }

    public void UndoAction() {
        start.RemoveConnection(end);
        end.RemoveConnection(start);
        wire.SetActive(false);
    }

    public void RedoAction() {
        DoAction();
    }

    public void OnDestroy() {
        Object.Destroy(wire);
    }
}
