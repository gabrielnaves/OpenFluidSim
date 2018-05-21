using UnityEngine;

public class PneumaticSolenoid : CorrelatedObject {

    [ViewOnly] public bool active;

    SpriteRenderer spriteRenderer;

    protected override CorrelationTarget[] GetCorrelationTargets() {
        return SimulationPanel.instance.GetActiveSolenoids();
    }

    protected override string CreateConfigWindowTitle() {
        return "Select a Solenoid:";
    }

    void Start() {
        GetComponentInParent<ComponentReferences>().AddPneumaticSolenoid(this);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Activate() {
        spriteRenderer.color = Color.green;
        active = true;
    }

    public void Deactivate() {
        spriteRenderer.color = Color.white;
        active = false;
    }
}
