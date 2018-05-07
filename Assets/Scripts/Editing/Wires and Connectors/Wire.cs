using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Visual representation of a pneumatic or electric connection
/// </summary>
/// Wires are mostly for visualization purposes, but they can be
/// selected and deleted.
/// When a wire is deleted the corresponding  pneumatic connection
/// is disabled as well.
/// Wires are currently implemented as simple straight lines. This
/// implementation will be improved if I have the time to do so.
public class Wire : MonoBehaviour, ISelectable {


    [ViewOnly] public Connector start;
    [ViewOnly] public Connector end;

    LineRenderer lineRenderer;
    ObjectPooler clickColliderPooler;
    Vector3[] points = new Vector3[2];
    List<BoxCollider2D> clickColliders = new List<BoxCollider2D>();

    bool ISelectable.RequestedSelect() {
        foreach (var collider in clickColliders)
            if (collider.OverlapPoint(SimulationInput.instance.mousePosition))
                return true;
        return false;
    }

    bool ISelectable.IsInsideSelectionBox(Collider2D selectionBox) {
        foreach (var collider in clickColliders)
            if (selectionBox.IsTouching(collider))
                return true;
        return false;
    }

    void ISelectable.OnSelect() {
        UpdateColor(Color.green);
    }

    void ISelectable.OnDeselect() {
        UpdateColor(Color.black);
    }

    void UpdateColor(Color color) {
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }

    void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        clickColliderPooler = GetComponent<ObjectPooler>();
    }

    void Start() {
        points[0] = start.transform.position;
        points[1] = end.transform.position;
        UpdateLineRenderer();
    }

    void OnEnable() {
        SimulationPanel.instance.AddWire(this);
        SimulationPanel.instance.AddSelectable(this);
    }

    void OnDisable() {
        SimulationPanel.instance.RemoveWire(this);
        SimulationPanel.instance.RemoveSelectable(this);
    }

    void LateUpdate() {
        if (ReferencesMoved())
            UpdateLineRenderer();
        if (SimulationMode.instance.mode == SimulationMode.Mode.simulation)
            UpdateColor(start.signal && end.signal ? Color.green : Color.red);
    }

    bool ReferencesMoved() {
        bool moved = false;
        if (start.transform.position != points[0]) {
            points[0] = start.transform.position;
            moved = true;
        }
        if (end.transform.position != points[1]) {
            points[1] = end.transform.position;
            moved = true;
        }
        return moved;
    }

    void UpdateLineRenderer() {
        lineRenderer.SetPositions(points);
        UpdateClickColliders();
    }

    void UpdateClickColliders() {
        ClearColliderlist();
        for (int i = 0; i < points.Length - 1; ++i) {
            BoxCollider2D newBox = clickColliderPooler.GetObject().GetComponent<BoxCollider2D>();
            newBox.transform.position = (points[i] + points[i+1]) / 2;
            newBox.transform.rotation =
                Quaternion.Euler(0, 0, Mathf.Atan2(points[i+1].y - points[i].y, points[i+1].x - points[i].x) * Mathf.Rad2Deg);
            newBox.size = new Vector2(Vector3.Distance(points[i], points[i+1]), 0.1f);
            newBox.gameObject.SetActive(true);
            clickColliders.Add(newBox);
        }
    }

    void ClearColliderlist() {
        foreach (BoxCollider2D collider in clickColliders)
            clickColliderPooler.ReturnObject(collider.gameObject);
        clickColliders.Clear();
    }
}
