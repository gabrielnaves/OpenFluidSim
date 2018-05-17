public class Contact : CorrelatedObject {

    public enum Type { coil, sensor }
    public enum State { open, closed, none }

    public Type type = Type.coil;
    public State state = State.open;

    protected override CorrelationTarget[] GetCorrelationTargets() {
        if (type == Type.coil)
            return SimulationPanel.instance.GetActiveCoils();
        else
            return SimulationPanel.instance.GetActiveSensors();
    }

    protected override string CreateConfigWindowTitle() {
        if (type == Type.coil)
            return "Selecione uma contatora abaixo:";
        else
            return "Selecione um sensor abaixo:";
    }
}
