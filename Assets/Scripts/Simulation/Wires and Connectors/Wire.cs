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
public class Wire : MonoBehaviour {

    public bool wireEnabled = false;
    public List<Vector3> points = new List<Vector3>();
    public PneumaticConnector start;
    public PneumaticConnector end;
    
    public List<BoxCollider2D> clickColliders = new List<BoxCollider2D>();

    LineRenderer lineRenderer;
    //ComponentSelect componentSelect;

    void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        //componentSelect = new ComponentSelect();
    }

    void Update() {
        //componentSelect.Update();
        //if (componentSelect.isSelected && Input.GetKeyDown(KeyCode.Delete))
        //    ActionStack.instance.PushAction(new DeleteWireAction(this));
    }

    void LateUpdate() {
        //if (AttachedComponentsEnabled())
        //    wireEnabled = true;
        //if (wireEnabled) {
        //    if (!AttachedComponentsEnabled()) {
        //        lineRenderer.startColor = Color.clear;
        //        lineRenderer.endColor = Color.clear;
        //        wireEnabled = false;
        //    }
        //    else if (componentSelect.isSelected) {
        //        lineRenderer.startColor = Color.green;
        //        lineRenderer.endColor = Color.green;
        //    }
        //    else {
        //        lineRenderer.startColor = Color.black;
        //        lineRenderer.endColor = Color.black;
        //    }
        //    if (AttachedComponentsMoved()) {
        //        points[0] = start.transform.position;
        //        points[points.Count-1] = end.transform.position;
        //        UpdateLineRenderer();
        //    }
        //}
    }

    bool AttachedComponentsEnabled() {
        if (start && end)
            return start.isActiveAndEnabled && end.isActiveAndEnabled;
        return false;
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
        //componentSelect = new ComponentSelect(gameObject, clickColliders.ToArray());
    }

    void DeleteAllColliders() {
        foreach(BoxCollider2D collider in clickColliders)
            Destroy(collider.gameObject);
        clickColliders.Clear();
    }
}
