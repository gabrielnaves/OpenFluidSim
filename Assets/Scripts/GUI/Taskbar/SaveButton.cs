using UnityEngine;

public class SaveButton : TaskbarButton {

    public GameObject saveWindowPrefab;

    protected override bool ShouldShowButton() {
        return SimulationPanel.instance.activeComponents.Count > 0;
    }

    public void SaveSimulation() {
#if UNITY_WEBGL && !UNITY_EDITOR
        Instantiate(saveWindowPrefab);
#else
        SaveUtility.instance.SaveSimulationToFile();
#endif
    }
}
