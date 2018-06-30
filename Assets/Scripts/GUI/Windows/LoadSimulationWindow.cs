using UnityEngine;
using UnityEngine.UI;

public class LoadSimulationWindow : MonoBehaviour {

    public InputField inputField;

    public void LoadSimulation() {
        Clipboard.SetClipboard(inputField.text);
        LoadUtility.instance.LoadFromClipboard();
        Destroy(gameObject);
    }

    public void CloseWindow() {
        Destroy(gameObject);
    }

    void Start() {
        SelectedObjects.instance.ClearSelection();
        EditorInput.instance.gameObject.SetActive(false);
        ComponentListBar.instance.Disable();
        Taskbar.instance.Disable();
        CameraControlsGUI.instance.Disable();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            CloseWindow();
    }

    void OnDestroy() {
        EditorInput.instance.gameObject.SetActive(true);
        ComponentListBar.instance.Enable();
        Taskbar.instance.Enable();
        CameraControlsGUI.instance.Enable();
    }
}
