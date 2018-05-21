using UnityEngine;
using UnityEngine.UI;

public class Taskbar : MonoBehaviour {

    static public Taskbar instance { get; private set; }

    public Button[] editorButtons;
    public Button[] simulationButtons;

    [ViewOnly] public bool taskbarEnabled;

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
        taskbarEnabled = true;
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
        taskbarEnabled = false;
    }

    public void Enable() {
        taskbarEnabled = true;
    }
}
