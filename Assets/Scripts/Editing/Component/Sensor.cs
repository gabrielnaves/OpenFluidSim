public class Sensor : CorrelationTarget {

    void OnEnable() {
        SimulationPanel.instance.AddSensor(this);
    }

    void OnDisable() {
        SimulationPanel.instance.RemoveSensor(this);
    }

    void LateUpdate() {
        int indexOnSensorList = SimulationPanel.instance.activeSensors.IndexOf(this);
        nameText.text = "Sensor " + (indexOnSensorList + 1);
    }
}
