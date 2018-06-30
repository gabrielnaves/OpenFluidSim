using System.Collections.Generic;
using UnityEngine;

public class Valve_5_3_Simulation : FluidComponentSimulation {

    public Transform[] componentsToAnimate;
    public float shiftAmount;

    public enum ValveState { left, middle, right }
    [ViewOnly] public ValveState state = ValveState.middle;

    Dictionary<ValveState, int[]> internalConnections = new Dictionary<ValveState, int[]> {
        { ValveState.left, new int[]{ 3, 4, -1, 0, 1 } },
        { ValveState.middle, new int[]{ -1, -1, -1, -1, -1 } },
        { ValveState.right, new int[]{ 2, 3, 0, 1, -1 } },
    };

    Vector3[] originalPositions;
    ComponentReferences componentReferences;
    bool simulating;

    void Awake() {
        componentReferences = GetComponent<ComponentReferences>();
    }

    public override void Setup() {
        state = ValveState.middle;
        originalPositions = new Vector3[componentsToAnimate.Length];
        for (int i = 0; i < componentsToAnimate.Length; ++i)
            originalPositions[i] = componentsToAnimate[i].position;
        simulating = true;
    }

    public override void Stop() {
        state = ValveState.middle;
        for (int i = 0; i < componentsToAnimate.Length; ++i)
            componentsToAnimate[i].position = originalPositions[i];
        originalPositions = null;
        simulating = false;
    }

    public override void RespondToSignal(Connector sourceConnector, float signal) {
        var connectorList = componentReferences.connectorList;
        int sourceConnectorIndex = connectorList.IndexOf(sourceConnector);
        int targetConnectorIndex = internalConnections[state][sourceConnectorIndex];
        if (targetConnectorIndex != -1)
            FluidSimulationEngine.instance.SpreadSignal(connectorList[targetConnectorIndex], signal);
    }

    void LateUpdate() {
        if (simulating) {
            PneumaticSolenoid leftSolenoid = componentReferences.solenoidList[0];
            PneumaticSolenoid rightSolenoid = componentReferences.solenoidList[1];
            if (leftSolenoid.active && !rightSolenoid.active) {
                if (state != ValveState.left) {
                    state = ValveState.left;
                    for (int i = 0; i < componentsToAnimate.Length; ++i)
                        componentsToAnimate[i].position = originalPositions[i] + transform.right * shiftAmount;
                }
            }
            else if (!leftSolenoid.active && rightSolenoid.active) {
                if (state != ValveState.right) {
                    state = ValveState.right;
                    for (int i = 0; i < componentsToAnimate.Length; ++i)
                        componentsToAnimate[i].position = originalPositions[i] - transform.right * shiftAmount;
                }
            }
            else if (state != ValveState.middle) {
                state = ValveState.middle;
                for (int i = 0; i < componentsToAnimate.Length; ++i)
                    componentsToAnimate[i].position = originalPositions[i];
            }
        }
    }
}
