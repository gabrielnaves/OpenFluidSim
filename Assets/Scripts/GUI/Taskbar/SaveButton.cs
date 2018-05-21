public class SaveButton : TaskbarButton {

    protected override bool ShouldShowButton() {
        return SimulationPanel.instance.activeComponents.Count > 0;
    }

    public void SaveSimulation() {
#if UNITY_WEBGL && !UNITY_EDITOR

#else
        SaveUtility.instance.SaveSimulationToFile();
#endif
    }
}
