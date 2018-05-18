public class SimulationControlButton : TaskbarButton {

    protected override bool ShouldShowButton() {
        return SimulationPanel.instance.activeComponents.Count > 0;
    }
}
