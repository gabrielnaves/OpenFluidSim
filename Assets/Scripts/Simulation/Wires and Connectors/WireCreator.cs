using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireCreator : MonoBehaviour {

    public static WireCreator instance { get; private set; }

    public GameObject wirePrefab;

    public bool running;
    public GameObject currentWire;
    public List<Vector3> currentPointList = new List<Vector3>();
    public int currentPoint = 1;

    void Awake() {
        instance = (WireCreator)Singleton.Setup(this, instance);
    }

    void Start() {
        currentWire = Instantiate(wirePrefab);
        currentWire.transform.parent = transform;
        currentWire.SetActive(false);
    }

    public void StartGeneration(Vector3 initialPoint) {
        currentPointList.Clear();
        currentPointList.Add(initialPoint);
        currentPointList.Add(Vector3.zero);
        currentPoint = 1;
        currentWire.SetActive(true);
        running = true;
    }

    public void StopGeneration() {
        currentPointList.Clear();
        currentWire.SetActive(false);
        running = false;
    }
 
    /// <summary>
    /// Returns a duplicate of the current wire.
    /// </summary>
    public GameObject RetrieveWire(Vector3 lastPosition) {
        GameObject result = Instantiate(currentWire);
        Wire wire = result.GetComponent<Wire>();
        result.transform.parent = transform;
        wire.points[wire.points.Count-1] = lastPosition;
        return result;
    }

    void LateUpdate() {
        if (running) {
            currentPointList[currentPoint] = SimulationGrid.FitToGrid(SimulationInput.instance.GetMousePosition());
            currentWire.GetComponent<Wire>().points = currentPointList;
        }
    }
}
