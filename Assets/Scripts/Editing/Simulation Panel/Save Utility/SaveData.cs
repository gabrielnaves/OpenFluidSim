using System;
using UnityEngine;

[Serializable]
public class SavedData {
    public SavedComponentData[] components;
}

[Serializable]
public class SavedComponentData {
    public string name;
    public int componentId;
    public Vector3 position;
    public SavedConnectorData[] connectors;
    public SavedSolenoidData[] solenoids;
    public bool isContact;
    public int contactTargetId;
    public SavedCylinderData cylinderData;
    public SavedCoilData coilData;
}

[Serializable]
public class SavedConnectorData {
    public int connectorId;
    public int[] otherConnectorIds;
}

[Serializable]
public class SavedSolenoidData {
    public bool configured;
    public int solenoidTargetId;
}

[Serializable]
public class SavedCylinderData {
    public float startingPercentage;
    public float movementTime;
}

[Serializable]
public class SavedCoilData {
    public int coilType;
    public float delay;
}

