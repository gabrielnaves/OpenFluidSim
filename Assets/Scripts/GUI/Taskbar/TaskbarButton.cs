using UnityEngine;
using UnityEngine.UI;

public abstract class TaskbarButton : MonoBehaviour {

    Button button;

    void Awake() {
        button = GetComponent<Button>();
    }

    void Update() {
        if (ShouldShowButton())
            ShowButton();
        else
            HideButton();
    }

    protected abstract bool ShouldShowButton();

    void ShowButton() {
        button.interactable = true;
    }

    void HideButton() {
        button.interactable = false;
    }
}
