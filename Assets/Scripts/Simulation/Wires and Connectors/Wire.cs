using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour {

    public List<Vector3> points = new List<Vector3>();
    public PneumaticConnector start;
    public PneumaticConnector end;

    LineRenderer lineRenderer;

    void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void LateUpdate() {
        if (start && end) {
            if (!start.isActiveAndEnabled || !end.isActiveAndEnabled) {
                lineRenderer.startColor = Color.clear;
                lineRenderer.endColor = Color.clear;
            }
            else {
                lineRenderer.startColor = Color.black;
                lineRenderer.endColor = Color.black;
            }
            points[0] = start.transform.position;
            points[points.Count-1] = end.transform.position;
        }
        lineRenderer.SetPositions(points.ToArray());
    }
}
