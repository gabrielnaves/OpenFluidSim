public class Coil : CorrelationTarget {

    void OnEnable() {
        SimulationPanel.instance.AddCoil(this);
    }

    void OnDisable() {
        SimulationPanel.instance.RemoveCoil(this);
    }

    void LateUpdate() {
        int indexOnCoilList = SimulationPanel.instance.activeCoils.IndexOf(this);
        nameText.text = "K" + (indexOnCoilList + 1);
    }

    public override string TypeString() {
        return "Coil";
    }
}
