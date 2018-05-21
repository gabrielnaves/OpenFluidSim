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
            return "Select a coil:";
        else
            return "Select a sensor:";
    }
}
