using UnityEngine;

public class LoadButton : TaskbarButton {

    public GameObject loadWindowPrefab;

    public void LoadSimulation() {
#if UNITY_WEBGL && !UNITY_EDITOR
        Instantiate(loadWindowPrefab);
#else
        LoadUtility.instance.LoadFromFile();
#endif
    }
}
