using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Substitutes dummy connectors on component prefabs for pneumatic connectors
/// </summary>
/// This script circumvents Unity's lack of support for nested prefabs,
/// that is, prefabs that are part of other prefabs, but still keep their
/// independence.
/// This way many component prefabs can make use of the single pneumatic connector
/// prefab.
/// TODO: Merge this implementation with ComponentContacts
public class ComponentConnectors : MonoBehaviour {

    public List<PneumaticConnector> connectorList { get; private set; }
    public GameObject pneumaticConnectorPrefab;

    private List<Vector3> connectorLocalPositions = new List<Vector3>();

    void Awake() {
        GetConnectorPositions();
        RemoveDummyConnectors();
        CreatePneumaticConnectors();
    }

    private void GetConnectorPositions() {
        foreach (Transform child in transform)
            connectorLocalPositions.Add(child.localPosition);
    }

    private void RemoveDummyConnectors() {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    private void CreatePneumaticConnectors() {
        connectorList = new List<PneumaticConnector>();
        foreach (Vector3 localPositions in connectorLocalPositions) {
            var newConnector = Instantiate(pneumaticConnectorPrefab);
            newConnector.transform.parent = transform;
            newConnector.transform.localPosition = localPositions;
            connectorList.Add(newConnector.GetComponent<PneumaticConnector>());
        }
    }
}
