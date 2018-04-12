using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the reference to the currently selected components or wires
/// </summary>
public class SelectedObjects : MonoBehaviour {

    static public SelectedObjects instance { get; private set; }

    /// <summary>
    /// Currently selected components or wires
    /// </summary>
    List<ISelectable> objects = new List<ISelectable>();

    public bool HasObject() {
        return objects.Count > 0;
    }

    public bool IsSelected(ISelectable obj) {
        return objects.Contains(obj);
    }

    public void SelectComponent(ISelectable obj) {
        if (!IsSelected(obj)) {
            objects.Add(obj);
            obj.OnSelect();
        }
    }

    public void DeselectComponent(ISelectable obj) {
        if (IsSelected(obj)) {
            objects.Remove(obj);
            obj.OnDeselect();
        }
    }

    public void ClearSelection() {
        foreach (var obj in objects)
            obj.OnDeselect();
        objects.Clear();
    }

    void Awake() {
        instance = (SelectedObjects)Singleton.Setup(this, instance);
    }
}
