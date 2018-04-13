using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates Wire objects
/// </summary>
///  Wires are currently implemented as simple straight lines. This
/// implementation will be improved if I have the time to do so. 
public class WireCreator : MonoBehaviour {

    public static WireCreator instance { get; private set; }

    public GameObject dummyWirePrefab;
    public GameObject wirePrefab;

    bool running;
    DummyWire dummyWire;
    Connector start;

    void Awake() {
        instance = (WireCreator)Singleton.Setup(this, instance);
    }

    void Start() {
        dummyWire = Instantiate(dummyWirePrefab).GetComponent<DummyWire>();
        dummyWire.transform.parent = transform;
        dummyWire.gameObject.SetActive(false);
    }

    public void StartGeneration(Connector start) {
        this.start = start;
        dummyWire.points = new List<Vector3>() {
            start.transform.position, SimulationInput.instance.mousePosition
        };
        dummyWire.gameObject.SetActive(true);
        running = true;
    }

    public void StopGeneration() {
        dummyWire.gameObject.SetActive(false);
        running = false;
    }
 
    /// <summary>
    /// Returns a duplicate of the current wire.
    /// </summary>
    public Wire RetrieveWire(Connector end) {
        Wire wire = Instantiate(wirePrefab).GetComponent<Wire>();
        wire.start = start;
        wire.end = end;
        return wire;
    }

    void LateUpdate() {
        if (running)
            dummyWire.points[1] = SimulationGrid.FitToGrid(SimulationInput.instance.mousePosition);
    }
}
