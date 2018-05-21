using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListMessageWindow : MonoBehaviour {

    public GameObject textBox;
    public Text title;
    public ContentsManager contentsManager;

    [ViewOnly] public string[] listItems;
    [ViewOnly] public string windowTitle;

    public void CloseWindow() {
        Destroy(gameObject);
    }

    void Start() {
        SelectedObjects.instance.ClearSelection();
        EditorInput.instance.gameObject.SetActive(false);
        ComponentListBar.instance.Disable();
        Taskbar.instance.Disable();
        GenerateListEntries();
        title.text = windowTitle;
    }

    void OnDestroy() {
        if (EditorInput.instance)
            EditorInput.instance.gameObject.SetActive(true);
        ComponentListBar.instance.Enable();
        Taskbar.instance.Enable();
    }

    void GenerateListEntries() {
        foreach (var item in listItems) {
            GameObject newListItem = Instantiate(textBox);
            newListItem.GetComponent<Text>().text = item;
            contentsManager.AddToContents(newListItem);
            newListItem.SetActive(true);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            CloseWindow();
    }
}
