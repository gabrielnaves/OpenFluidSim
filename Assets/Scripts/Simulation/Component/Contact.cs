using UnityEngine;
using UnityEngine.UI;

public class Contact : MonoBehaviour, IConfigurable {

    public enum ContactType { open, closed }

    public ContactType type = ContactType.open;
    public Text nameText;
    public GameObject configWindowPrefab;

    [ViewOnly] public Coil correspondingCoil;

    new Collider2D collider;

    bool IConfigurable.RequestedConfig() {
        return SimulationInput.instance.doubleClick &&
            collider.OverlapPoint(SimulationInput.instance.mousePosition);
    }

    void IConfigurable.OpenConfigWindow() {
        if (SimulationPanel.instance.GetActiveCoils().Length > 0)
            Instantiate(configWindowPrefab).GetComponent<ContactConfigWindow>().contact = this;
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
        if (correspondingCoil == null)
            nameText.text = "--";
        else if (!correspondingCoil.gameObject.activeInHierarchy)
            nameText.text = "--";
        else
            nameText.text = correspondingCoil.coilName;
    }
}
