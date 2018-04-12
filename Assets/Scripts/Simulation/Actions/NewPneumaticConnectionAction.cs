using UnityEngine;

/// <summary>
/// Action for connecting two pneumatic connectors
/// </summary>
/// A single wire object is also linked to the connection.
public class NewPneumaticConnectionAction : IAction {

    //PneumaticConnector start;
    //PneumaticConnector end;
    GameObject wire;

    public NewPneumaticConnectionAction(PneumaticConnector start, PneumaticConnector end, GameObject wire) {
        this.wire = wire;
        //this.start = start;
        //this.end = end;
    }

    public void DoAction() {
        //start.AddConnection(end);
        //end.AddConnection(start);
        wire.SetActive(true);
        SimulationPanel.instance.AddWire(wire.GetComponent<Wire>());
    }

    public void UndoAction() {
        //start.RemoveConnection(end);
        //end.RemoveConnection(start);
        wire.SetActive(false);
        SimulationPanel.instance.RemoveWire(wire.GetComponent<Wire>());
    }

    public void RedoAction() {
        DoAction();
    }

    public void OnDestroy() {
        Object.Destroy(wire);
    }
}
