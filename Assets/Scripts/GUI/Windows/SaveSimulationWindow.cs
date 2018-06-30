using UnityEngine;
using UnityEngine.UI;

public class SaveSimulationWindow : MonoBehaviour {

    public InputField inputField;

    public void CloseWindow() {
        Destroy(gameObject);
    }

    public void SelectAll() {
        inputField.Select();
    }

    void Start() {
        SelectedObjects.instance.ClearSelection();
        EditorInput.instance.gameObject.SetActive(false);
        ComponentListBar.instance.Disable();
        Taskbar.instance.Disable();
        CameraControlsGUI.instance.Disable();

        inputField.text = SaveUtility.instance.GetSimulationSaveString();
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
