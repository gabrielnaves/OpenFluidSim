using UnityEngine;
using UnityEngine.UI;

public abstract class TaskbarButton : MonoBehaviour {

    Image image;
    Button button;

    void Awake() {
        image = GetComponent<Image>();
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
        image.color = Color.white;
        button.enabled = true;
    }

    void HideButton() {
        image.color = Color.clear;
        button.enabled = false;
    }
}
