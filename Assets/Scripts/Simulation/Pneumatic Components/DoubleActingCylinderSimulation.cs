using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleActingCylinderSimulation : PneumaticComponentSimulation {

    public Transform[] cylinderParts;
    public float movementSpeed;
    public float maxDisplacement;
    public GameObject configWindow;

    ComponentReferences componentReferences;
    Vector3[] originalPositions;
    float displacement;
    float[] signals = new float[2];
    bool simulating;

    void Awake() {
        componentReferences = GetComponent<ComponentReferences>();
    }

    public override void Setup() {
        displacement = 0;
        originalPositions = new Vector3[cylinderParts.Length];
        for (int i = 0; i < cylinderParts.Length; ++i)
            originalPositions[i] = cylinderParts[i].position;
        simulating = true;
    }

    public override void Stop() {
        for (int i = 0; i < cylinderParts.Length; ++i)
            cylinderParts[i].position = originalPositions[i];
        originalPositions = null;
        simulating = false;
    }

    public override void RespondToSignal(Connector sourceConnector, float signal) {
        var connectorList = componentReferences.connectorList;
        int sourceConnectorIndex = connectorList.IndexOf(sourceConnector);
        signals[sourceConnectorIndex] = signal;
    }

    void FixedUpdate() {
        if (simulating) {
            bool move = signals[0] != signals[1];
            movementSpeed = Mathf.Abs(movementSpeed) * (signals[0] > signals[1] ? 1f : -1f);
            if (move) {
                displacement += movementSpeed * Time.fixedDeltaTime;
                displacement = Mathf.Clamp(displacement, 0, maxDisplacement);
                for (int i = 0; i < cylinderParts.Length; ++i)
                    cylinderParts[i].position = originalPositions[i] + transform.right * displacement;
            }
        }
    }
}
