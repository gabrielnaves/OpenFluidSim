using UnityEngine;

/// <summary>
/// Generates Wire objects
/// </summary>
///  Wires are currently implemented as simple straight lines. This
/// implementation will be improved if I have the time to do so. 
public class WireCreator : MonoBehaviour {

    public static WireCreator instance { get; private set; }

    public GameObject wirePrefab;

    bool running;
    Wire currentWire;

    void Awake() {
        instance = (WireCreator)Singleton.Setup(this, instance);
    }

    void Start() {
        currentWire = Instantiate(wirePrefab).GetComponent<Wire>();
        currentWire.transform.parent = transform;
        currentWire.gameObject.SetActive(false);
    }

    public void StartGeneration(Vector3 initialPoint) {
        currentWire.points.Clear();
        currentWire.points.Add(initialPoint);
        currentWire.points.Add(Vector3.zero);
        currentWire.gameObject.SetActive(true);
        running = true;
    }

    public void StopGeneration() {
        currentWire.points.Clear();
        currentWire.gameObject.SetActive(false);
        running = false;
    }
 
    /// <summary>
    /// Returns a duplicate of the current wire.
    /// </summary>
    public GameObject RetrieveWire(Vector3 lastPosition) {
        GameObject result = Instantiate(currentWire).gameObject;
        Wire wire = result.GetComponent<Wire>();
        result.transform.parent = transform;
        wire.points[1] = lastPosition;
        wire.wireEnabled = true;
        return result;
    }

    void LateUpdate() {
        if (running) {
            currentWire.points[1] = SimulationGrid.FitToGrid(SimulationInput.instance.mousePosition);
            currentWire.UpdateLineRenderer();
        }
    }
}
