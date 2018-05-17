using UnityEngine;
using UnityEngine.UI;

public class Contact : MonoBehaviour, IConfigurable {

    public enum Type { coil, sensor }
    public enum State { open, closed, none }

    public Type type = Type.coil;
    public State state = State.open;
    public Text nameText;
    public GameObject configWindowPrefab;

    [ViewOnly] public ContactEnabler correlatedContact;

    new Collider2D collider;

    bool IConfigurable.RequestedConfig() {
        return SimulationInput.instance.doubleClick &&
            collider.OverlapPoint(SimulationInput.instance.mousePosition);
    }

    void IConfigurable.OpenConfigWindow() {
        ContactEnabler[] contactEnablers;
        if (type == Type.coil)
            contactEnablers = SimulationPanel.instance.GetActiveCoils();
        else
            contactEnablers = SimulationPanel.instance.GetActiveSensors();

        if (contactEnablers.Length > 0)
            CreateConfigWindow(contactEnablers);
    }

    bool IConfigurable.IsConfigured() {
        if (correlatedContact != null)
            if (correlatedContact.gameObject.activeInHierarchy)
                return true;
        return false;
    }

    void CreateConfigWindow(ContactEnabler[] contactEnablers) {
        GameObject configWindowObj = Instantiate(configWindowPrefab);
        ContactConfigWindow configWindow = configWindowObj.GetComponent<ContactConfigWindow>();
        configWindow.contact = this;
        configWindow.enablers = contactEnablers;
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
        if (correlatedContact == null)
            nameText.text = "--";
        else if (!correlatedContact.gameObject.activeInHierarchy)
            nameText.text = "--";
        else
            nameText.text = correlatedContact.nameStr;
    }
}
