﻿using UnityEngine;

public class SingleActingCylinderSimulation : FluidComponentSimulation {

    CylinderEditing cylinderEditing;

    Transform[] cylinderParts;
    Vector3[] originalPositions;

    float movementSpeed;
    float maxDisplacement;
    float displacement;
    float signal;
    bool gotSignal;
    bool simulating;

    void Awake() {
        cylinderEditing = GetComponent<CylinderEditing>();
    }

    public override void Setup() {
        cylinderParts = cylinderEditing.cylinderParts;
        originalPositions = cylinderEditing.originalPositions;
        maxDisplacement = cylinderEditing.GetMaxDisplacement();
        movementSpeed = maxDisplacement / cylinderEditing.movementTime;
        displacement = cylinderEditing.startingPercentage * maxDisplacement;
        simulating = true;
    }

    public override void Stop() {
        cylinderEditing.UpdateSprites();
        simulating = false;
    }

    public override void RespondToSignal(Connector sourceConnector, float signal) {
        this.signal = signal;
        gotSignal = true;
    }

    void FixedUpdate() {
        if (simulating) {
            if (!gotSignal) signal = 0;
            bool move = signal > 0;
            movementSpeed = Mathf.Abs(movementSpeed);
            if (move) {
                displacement += movementSpeed * Time.fixedDeltaTime;
                displacement = Mathf.Clamp(displacement, 0, maxDisplacement);
                for (int i = 0; i < cylinderParts.Length; ++i)
                    cylinderParts[i].localPosition = originalPositions[i] + transform.right * displacement;
            }
            gotSignal = false;
        }
    }
}
