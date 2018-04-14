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
}

[Serializable]
public class SavedConnectorData {
    public int connectorId;
    public int[] otherConnectorIds;
}
