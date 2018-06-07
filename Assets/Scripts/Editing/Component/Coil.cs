using UnityEngine;

public class Coil : CorrelationTarget, IConfigurable {

    public enum Type { normal, on_delay, off_delay }

    public GameObject coilConfigWindowPrefab;
    public Sprite normalCoilSprite;
    public Sprite onDelaySprite;
    public Sprite offDelaySprite;
    [ViewOnly] public Type type = Type.normal;
    [ViewOnly] public float delay = 1f;

    new Collider2D collider;
    SpriteRenderer spriteRenderer;

    void Awake() {
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable() {
        SimulationPanel.instance.AddCoil(this);
        SimulationPanel.instance.AddConfigurable(this);
    }

    void OnDisable() {
        SimulationPanel.instance.RemoveCoil(this);
        SimulationPanel.instance.RemoveConfigurable(this);
    }

    void LateUpdate() {
        int indexOnCoilList = SimulationPanel.instance.activeCoils.IndexOf(this);
        nameText.text = "K" + (indexOnCoilList + 1);
    }

    public override string TypeString() {
        return "Coil";
    }

    bool IConfigurable.RequestedConfig() {
        return EditorInput.instance.doubleClick &&
            collider.OverlapPoint(EditorInput.instance.mousePosition);
    }

    void IConfigurable.OpenConfigWindow() {
        var coilConfigWindow = Instantiate(coilConfigWindowPrefab).GetComponent<CoilConfigWindow>();
        coilConfigWindow.coil = this;
    }

    bool IConfigurable.IsConfigured() {
        return true;
    }

    public void UpdateType(Type type) {
        this.type = type;
        switch (type) {
            case Type.normal:
                spriteRenderer.sprite = normalCoilSprite;
                break;
            case Type.on_delay:
                spriteRenderer.sprite = onDelaySprite;
                break;
            case Type.off_delay:
                spriteRenderer.sprite = offDelaySprite;
                break;
        }
    }

    public void UpdateDelay(float delay) {
        this.delay = delay;
    }
}
