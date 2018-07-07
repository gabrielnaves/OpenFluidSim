using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadSimulationWindow : MonoBehaviour {

    public GameObject baseButton;
    public GameObject errorMessage;
    public ContentsManager contentsManager;

    public void LoadSimulation(string fileName) {
        SaveUtility.instance.fileName = fileName;
        LoadUtility.instance.fileName = fileName;
        LoadUtility.instance.LoadFromFile();
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
        CreateFileButtons();
    }

    void CreateFileButtons() {
        try {
            var files = Directory.GetFiles(FolderPath.GetFolder(), "*.json");
            if (files.Length == 0)
                CreateErrorMessage();
            else {
                foreach (string f in files) {
                    string file = f.Remove(0, FolderPath.GetFolder().Length);
                    GameObject newButton = Instantiate(baseButton);
                    newButton.transform.GetChild(0).GetComponent<Text>().text = file;
                    contentsManager.AddToContents(newButton);
                    newButton.GetComponent<Button>().onClick.AddListener(() => {
                        LoadSimulation(file);
                    });
                    newButton.SetActive(true);
                }
            }
        }
        catch (DirectoryNotFoundException) {
            CreateErrorMessage();
        }
    }

    void CreateErrorMessage() {
        GameObject msg = Instantiate(errorMessage);
        msg.GetComponent<Text>().text = msg.GetComponent<Text>().text + FolderPath.GetFolder();
        contentsManager.AddToContents(msg);
        msg.SetActive(true);
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
