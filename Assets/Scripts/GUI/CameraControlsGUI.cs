using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlsGUI : MonoBehaviour {

    static public CameraControlsGUI instance { get; private set; }

    public GameObject[] controlObjects;

    bool cameraControlsEnabled = false;

    void Awake() {
        instance = (CameraControlsGUI)Singleton.Setup(this, instance);
    }

    void Start() {
        foreach (var obj in controlObjects)
            obj.SetActive(false);
    }

    public void Enable() {
        gameObject.SetActive(true);
    }

    public void Disable() {
        gameObject.SetActive(false);
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
