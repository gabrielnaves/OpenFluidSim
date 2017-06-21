using System.Collections.Generic;
using UnityEngine;

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
