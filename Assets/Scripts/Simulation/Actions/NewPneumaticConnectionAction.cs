using UnityEngine;

public class NewPneumaticConnectionAction : IAction {

    public PneumaticConnector connector1;
    public PneumaticConnector connector2;
    public GameObject wire;

    public void DoAction() {
        connector1.AddConnection(connector2);
        connector2.AddConnection(connector1);
        wire.SetActive(true);
    }

    public void UndoAction() {
        connector1.RemoveConnection(connector2);
        connector2.RemoveConnection(connector1);
        wire.SetActive(false);
    }

    public void RedoAction() {
        DoAction();
    }

    public void OnDestroy() {
        GameObject.Destroy(wire);
    }
}
