using UnityEngine;
using UnityEngine.UI;

public class Taskbar : MonoBehaviour {

    static public Taskbar instance { get; private set; }

    public Button[] editorButtons;
    public Button[] simulationButtons;

    public void StartedSimulation() {
        foreach (var button in editorButtons)
            DeactivateButton(button);
        foreach (var button in simulationButtons)
            ActivateButton(button);
    }

    public void StoppedSimulation() {
        foreach (var button in editorButtons)
            ActivateButton(button);
        foreach (var button in simulationButtons)
            DeactivateButton(button);
    }

    void Awake() {
        instance = (Taskbar)Singleton.Setup(this, instance);
    }

    void Start() {
        foreach (var button in simulationButtons)
            DeactivateButton(button);
    }

    void DeactivateButton(Button button) {
        button.gameObject.SetActive(false);
    }

    void ActivateButton(Button button) {
        button.gameObject.SetActive(true);
    }

    public void Disable() {
        GetComponent<CanvasGroup>().interactable = false;
    }

    public void Enable() {
        GetComponent<CanvasGroup>().interactable = true;
    }
}
