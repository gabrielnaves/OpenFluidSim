using UnityEngine;

public class SaveButton : TaskbarButton {

    public GameObject saveWindow;
    public GameObject webSaveWindow;

    protected override bool ShouldShowButton() {
        return SimulationPanel.instance.activeComponents.Count > 0;
    }

    public void SaveSimulation() {
#if UNITY_WEBGL && !UNITY_EDITOR
        Instantiate(webSaveWindow);
#else
        if (SaveUtility.instance.fileName != "")
            SaveUtility.instance.SaveSimulationToFile();
        else
            Instantiate(saveWindow);
#endif
    }

    public void SaveSimulationAs() {
#if UNITY_WEBGL && !UNITY_EDITOR
        Instantiate(webSaveWindow);
#else
        Instantiate(saveWindow);
#endif
    }
}
