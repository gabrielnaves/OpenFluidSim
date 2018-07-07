using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveSimulationWindow : MonoBehaviour {

    public GameObject saveConsole;
    public GameObject overwriteConsole;
    public InputField nameInputField;

    public void SaveSimulation() {
        FileInfo file = new FileInfo(FolderPath.GetFolder() + FileName());
        if (file.Exists) {
            saveConsole.SetActive(false);
            overwriteConsole.SetActive(true);
        }
        else
            OverwriteSimulation();
    }

    public void OverwriteSimulation() {
        SaveUtility.instance.fileName = FileName();
        SaveUtility.instance.SaveSimulationToFile();
        Destroy(gameObject);
    }

    string FileName() {
        string name = nameInputField.text.Replace(".json", "") + ".json";
        return name == "" ? "tmp.json" : name;
    }

    public void CancelOverwrite() {
        saveConsole.SetActive(true);
        overwriteConsole.SetActive(false);
        nameInputField.text = "";
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
        saveConsole.SetActive(true);
        overwriteConsole.SetActive(false);
        nameInputField.text = SaveUtility.instance.fileName.Replace(".json", "");
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
