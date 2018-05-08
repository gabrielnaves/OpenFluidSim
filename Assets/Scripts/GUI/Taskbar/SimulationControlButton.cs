using UnityEngine;
using UnityEngine.UI;

public class SimulationControlButton : MonoBehaviour {

    public SimulationMode.Mode targetMode;
    Image image;
    Button button;

    void Awake() {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    void Update() {
        if (SimulationMode.instance.mode == targetMode) {
            image.color = Color.white;
            button.enabled = true;
        }
        else {
            image.color = Color.clear;
            button.enabled = false;
        }
        if (SimulationPanel.instance.activeComponents.Count == 0) {
            image.color = Color.clear;
            button.enabled = false;
        }
    }
}
