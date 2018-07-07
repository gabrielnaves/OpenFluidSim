using UnityEngine;

public class CameraControlsGUI : MonoBehaviour {

    static public CameraControlsGUI instance { get; private set; }

    public GameObject[] controlObjects;

    CanvasGroup guiObject;
    bool cameraControlsEnabled = false;

    void Awake() {
        instance = (CameraControlsGUI)Singleton.Setup(this, instance);
        guiObject = GetComponentInChildren<CanvasGroup>();
    }

    void Start() {
        foreach (var obj in controlObjects)
            obj.SetActive(false);
    }

    public void Enable() {
        guiObject.interactable = true;
        GetComponent<CameraControls>().enabled = true;
    }

    public void Disable() {
        guiObject.interactable = false;
        GetComponent<CameraControls>().enabled = false;
    }

    public void ToggleCameraControls() {
        if (cameraControlsEnabled)
            DisableCameraControls();
        else
            EnableCameraControls();
    }

    void DisableCameraControls() {
        cameraControlsEnabled = false;
        if (SimulationMode.instance.mode == SimulationMode.Mode.editor) {
            ComponentListBar.instance.Enable();
            EditorInput.instance.gameObject.SetActive(true);
        }
        Taskbar.instance.Enable();
        SelectedObjects.instance.ClearSelection();
        foreach (var obj in controlObjects)
            obj.SetActive(false);
    }

    void EnableCameraControls() {
        cameraControlsEnabled = true;
        EditorInput.instance.gameObject.SetActive(false);
        BoxSelection.instance.StopSelecting();
        ComponentListBar.instance.Disable();
        Taskbar.instance.Disable();
        FloatingSelection.instance.RemoveCurrentComponent();
        SelectedObjects.instance.ClearSelection();
        foreach (var obj in controlObjects)
            obj.SetActive(true);
    }
}
