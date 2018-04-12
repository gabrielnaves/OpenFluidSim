using UnityEngine;

public class Contact : MonoBehaviour, IConfigurable {

    public enum ContactType { open, closed }
    public ContactType type = ContactType.open;

    [ViewOnly] public Coil correspondingCoil;

    new Collider2D collider;

    bool IConfigurable.RequestedConfig() {
        return SimulationInput.instance.doubleClick &&
            collider.OverlapPoint(SimulationInput.instance.mousePosition);
    }

    void IConfigurable.OpenConfigWindow() {
        Debug.Log("Open config window for " + name);
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
}
