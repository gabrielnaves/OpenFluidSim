using UnityEngine;

public class LoadButton : TaskbarButton {

    public GameObject loadWindow;
    public GameObject webLoadWindow;

    public void LoadSimulation() {
#if UNITY_WEBGL && !UNITY_EDITOR
        Instantiate(webLoadWindow);
#else
        Instantiate(loadWindow);
#endif
    }
}
