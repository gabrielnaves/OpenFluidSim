using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour {

    bool active = true;

    void Update() {
        if (SimulationPanel.instance.activeComponents.Count == 0 && active)
            DeactivateButton();
        if (SimulationPanel.instance.activeComponents.Count > 0 && !active)
            ActivateButton();
    }

    void DeactivateButton() {
        GetComponent<Image>().color = Color.clear;
        GetComponent<Button>().enabled = false;
        active = false;
    }

    void ActivateButton() {
        GetComponent<Image>().color = Color.white;
        GetComponent<Button>().enabled = true;
        active = true;
    }
}
