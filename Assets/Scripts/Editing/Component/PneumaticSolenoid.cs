public class PneumaticSolenoid : CorrelatedObject {

    protected override CorrelationTarget[] GetCorrelationTargets() {
        return SimulationPanel.instance.GetActiveSolenoids();
    }

    protected override string CreateConfigWindowTitle() {
        return "Selecione um Solenoide abaixo:";
    }

    void Start() {
        GetComponentInParent<ComponentReferences>().AddPneumaticSolenoid(this);
    }
}
