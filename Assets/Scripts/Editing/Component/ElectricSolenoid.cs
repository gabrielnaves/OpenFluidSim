using UnityEngine;
using UnityEngine.UI;

public class ElectricSolenoid : MonoBehaviour {

    public Text nameText;

    public string nameStr {
        get {
            return nameText.text;
        }
    }

    [ViewOnly] public bool activated = false;

    void OnEnable() {
        SimulationPanel.instance.AddSolenoid(this);
    }

    void OnDisable() {
        SimulationPanel.instance.RemoveSolenoid(this);
    }

    void LateUpdate() {
        int indexOnCoilList = SimulationPanel.instance.activeSolenoids.IndexOf(this);
        nameText.text = "Sol " + (indexOnCoilList + 1);
    }
}
