using UnityEngine;
using UnityEngine.UI;

public abstract class CorrelatedObject : MonoBehaviour, IConfigurable {

    public Text nameText;
    public GameObject configWindowPrefab;

    [ViewOnly] public CorrelationTarget correlationTarget;

    new protected Collider2D collider;

    public bool RequestedConfig() {
        return SimulationInput.instance.doubleClick &&
            collider.OverlapPoint(SimulationInput.instance.mousePosition);
    }

    public void OpenConfigWindow() {
        CorrelationTarget[] targets = GetCorrelationTargets();
        if (targets.Length > 0) {
            GameObject configWindowObj = Instantiate(configWindowPrefab);
            CorrelationConfigWindow configWindow = configWindowObj.GetComponent<CorrelationConfigWindow>();
            configWindow.correlatedObject = this;
            configWindow.correlationTargets = targets;
            configWindow.title.text = CreateConfigWindowTitle();
        }
    }

    protected abstract CorrelationTarget[] GetCorrelationTargets();

    protected abstract string CreateConfigWindowTitle();

    public bool IsConfigured() {
        if (correlationTarget != null)
            if (correlationTarget.gameObject.activeInHierarchy)
                return true;
        return false;
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
        if (correlationTarget == null)
            nameText.text = "--";
        else if (!correlationTarget.gameObject.activeInHierarchy)
            nameText.text = "--";
        else
            nameText.text = correlationTarget.nameStr;
    }
}
