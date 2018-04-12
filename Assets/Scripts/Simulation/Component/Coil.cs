using UnityEngine;
using UnityEngine.UI;

public class Coil : MonoBehaviour {

    public Text nameText;

    void OnEnable() {
        SimulationPanel.instance.AddCoil(this);
    }

    void OnDisable() {
        SimulationPanel.instance.RemoveCoil(this);
    }

    void LateUpdate() {
        int indexOnCoilList = SimulationPanel.instance.activeCoils.IndexOf(this);
        nameText.text = "K" + (indexOnCoilList + 1);
    }
}
