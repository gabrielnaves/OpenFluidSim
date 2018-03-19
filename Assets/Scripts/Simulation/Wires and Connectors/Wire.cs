using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour {

    public List<Vector3> points = new List<Vector3>();

    LineRenderer lineRenderer;

    void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void LateUpdate() {
        lineRenderer.SetPositions(points.ToArray());
    }
}
