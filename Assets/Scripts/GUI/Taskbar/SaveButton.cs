public class SaveButton : TaskbarButton {

    protected override bool ShouldShowButton() {
        return SimulationPanel.instance.activeComponents.Count > 0;
    }
}
