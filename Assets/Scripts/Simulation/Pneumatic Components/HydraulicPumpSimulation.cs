public class HydraulicPumpSimulation : FluidComponentSimulation {

    ComponentReferences componentReferences;

    void Awake() {
        componentReferences = GetComponent<ComponentReferences>();
    }

    public override void RespondToSignal(Connector sourceConnector, float signal) {
        if (sourceConnector == componentReferences.connectorList[0] && signal == -1)
            FluidSimulationEngine.instance.SpreadSignal(componentReferences.connectorList[1], 1f);
    }
}
