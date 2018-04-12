using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Visual representation of a pneumatic connection
/// </summary>
/// Wires are mostly for visualization purposes, but they can be
/// selected and deleted.
/// When a wire is deleted the corresponding  pneumatic connection
/// is disabled as well.
/// Wires are currently implemented as simple straight lines. This
/// implementation will be improved if I have the time to do so.
public class Wire : MonoBehaviour, ISelectable {

    public Connector start;
    public Connector end;

    bool isSelected;
    LineRenderer lineRenderer;
    Vector3[] points;
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
        isSelected = true;
    }

    void ISelectable.OnDeselect() {
        isSelected = false;
    }

    void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
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
        UpdateLineRenderer();
    }

    void UpdateLineRenderer() {
        points = new Vector3[] {
            start.transform.position, end.transform.position
        };
        lineRenderer.SetPositions(points);
        UpdateClickColliders();
        UpdateColor();
    }

    void UpdateClickColliders() {
        DeleteAllColliders();
        for (int i = 0; i < points.Length - 1; ++i) {
            BoxCollider2D newBox = new GameObject().AddComponent<BoxCollider2D>();
            newBox.name = "Wire click collider";
            newBox.transform.parent = transform;
            newBox.transform.position = (points[i] + points[i+1]) / 2;
            newBox.transform.rotation =
                Quaternion.Euler(0, 0, Mathf.Atan2(points[i+1].y - points[i].y, points[i+1].x - points[i].x) * Mathf.Rad2Deg);
            newBox.size = new Vector2(Vector3.Distance(points[i], points[i+1]), 0.1f);
            clickColliders.Add(newBox);
        }
    }

    void DeleteAllColliders() {
        foreach(BoxCollider2D collider in clickColliders)
            Destroy(collider.gameObject);
        clickColliders.Clear();
    }

    void UpdateColor() {
        Color color = isSelected ? Color.green : Color.black;
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }
}
