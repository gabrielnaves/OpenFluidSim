public class LoadButton : TaskbarButton {

    public void LoadSimulation() {
#if UNITY_WEBGL && !UNITY_EDITOR

#else
        LoadUtility.instance.LoadFromFile();
#endif
    }
}
