using UnityEngine;
using UnityEngine.UI;

public class TaskbarButton : MonoBehaviour {

    Button button;

    void Awake() {
        button = GetComponent<Button>();
    }

    void Update() {
        if (ShouldShowButton() && Taskbar.instance.taskbarEnabled)
            ShowButton();
        else
            HideButton();
    }

    protected virtual bool ShouldShowButton() { return true; }

    void ShowButton() {
        button.interactable = true;
    }

    void HideButton() {
        button.interactable = false;
    }
}
