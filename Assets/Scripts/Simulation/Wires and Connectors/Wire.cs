using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour {

    public bool wireEnabled = false;
    public List<Vector3> points = new List<Vector3>();
    public PneumaticConnector start;
    public PneumaticConnector end;

    LineRenderer lineRenderer;

    public List<BoxCollider2D> clickColliders = new List<BoxCollider2D>();

    void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update() {
        if (SimulationInput.instance.mouseButtonDown) {
            bool clickedOnWire = false;
            foreach(BoxCollider2D collider in clickColliders) {
                if (collider.OverlapPoint(SimulationInput.instance.mousePosition)) {
                    SelectedComponent.instance.component = gameObject;
                    clickedOnWire = true;
                    break;
                }
            }
            if (!clickedOnWire && SelectedComponent.instance.IsSelected(gameObject))
                SelectedComponent.instance.component = null;
        }
        if (SelectedComponent.instance.IsSelected(gameObject) && Input.GetKeyDown(KeyCode.Delete))
            ActionStack.instance.PushAction(new DeleteWireAction(this));
    }

    void LateUpdate() {
        if (wireEnabled) {
            if (!AttachedComponentsEnabled()) {
                lineRenderer.startColor = Color.clear;
                lineRenderer.endColor = Color.clear;
            }
            else if (SelectedComponent.instance.IsSelected(gameObject)) {
                lineRenderer.startColor = Color.green;
                lineRenderer.endColor = Color.green;
            }
            else {
                lineRenderer.startColor = Color.black;
                lineRenderer.endColor = Color.black;
            }
            if (AttachedComponentsMoved()) {
                points[0] = start.transform.position;
                points[points.Count-1] = end.transform.position;
                UpdateLineRenderer();
            }
        }
    }

    bool AttachedComponentsEnabled() {
        return start.isActiveAndEnabled && end.isActiveAndEnabled;
    }

    bool AttachedComponentsMoved() {
        return points[0] != start.transform.position || points[points.Count-1] != end.transform.position;
    }

    public void UpdateLineRenderer() {
        lineRenderer.SetPositions(points.ToArray());
        UpdateClickColliders();
    }

    void UpdateClickColliders() {
        DeleteAllColliders();
        for (int i = 0; i < points.Count - 1; ++i) {
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
}
