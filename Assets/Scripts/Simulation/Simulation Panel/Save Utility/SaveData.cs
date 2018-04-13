using System;
using UnityEngine;

[Serializable]
public class BaseComponentData {
    public string[] components;
    public Vector3[] positions;
    public int[] componentIds;
    public ComponentConnectionsData[] connections;
}

[Serializable]
public class ComponentConnectionsData {
    public int thisComponentId;
    public int[] connectorIds;
    public ConnectorData[] connectors;
}

[Serializable]
public class ConnectorData {
    public int parentConnectionId;
    public int thisConnectorId;
    public int[] otherConnectorIds;
}
