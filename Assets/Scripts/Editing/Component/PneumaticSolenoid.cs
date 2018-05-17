using UnityEngine;
using UnityEngine.UI;

public class PneumaticSolenoid : MonoBehaviour, IConfigurable {

    public Text nameText;
    public GameObject configWindowPrefab;

    [ViewOnly] public ElectricSolenoid correlatedSolenoid;

    new Collider2D collider;

    bool IConfigurable.RequestedConfig() {
        return SimulationInput.instance.doubleClick &&
            collider.OverlapPoint(SimulationInput.instance.mousePosition);
    }

    void IConfigurable.OpenConfigWindow() {
        ElectricSolenoid[] electricSolenoids;
        electricSolenoids = SimulationPanel.instance.GetActiveSolenoids();

        if (electricSolenoids.Length > 0)
            CreateConfigWindow(electricSolenoids);
    }

    bool IConfigurable.IsConfigured() {
        if (correlatedSolenoid != null)
            if (correlatedSolenoid.gameObject.activeInHierarchy)
                return true;
        return false;
    }

    void CreateConfigWindow(ElectricSolenoid[] electricSolenoids) {
        GameObject configWindowObj = Instantiate(configWindowPrefab);
        SolenoidConfigWindow configWindow = configWindowObj.GetComponent<SolenoidConfigWindow>();
        configWindow.pneumaticSolenoid = this;
        configWindow.electricSolenoids = electricSolenoids;
    }

    void Awake() {
        collider = GetComponent<Collider2D>();
    }

    void OnEnable() {
        SimulationPanel.instance.AddConfigurable(this);
    }

    void OnDisable() {
        SimulationPanel.instance.RemoveConfigurable(this);
    }

    void LateUpdate() {
        if (correlatedSolenoid == null)
            nameText.text = "--";
        else if (!correlatedSolenoid.gameObject.activeInHierarchy)
            nameText.text = "--";
        else
            nameText.text = correlatedSolenoid.nameStr;
    }
}
