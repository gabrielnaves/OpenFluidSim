using System.Collections.Generic;
using UnityEngine;

public class DummyWire : MonoBehaviour {

    public List<Vector3> points = new List<Vector3>();

    LineRenderer lineRenderer;

    void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void OnEnable() {
        lineRenderer.SetPositions(points.ToArray());
    }

    void Update() {
        lineRenderer.SetPositions(points.ToArray());
    }
}
