using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the reference to the currently selected components or wires
/// </summary>
public class SelectedObjects : MonoBehaviour {

    static public SelectedObjects instance { get; private set; }

    /// <summary>
    /// Currently selected objects
    /// </summary>
    List<ISelectable> selectedObjects = new List<ISelectable>();

    public bool HasObject() {
        return selectedObjects.Count > 0;
    }

    public bool IsSelected(ISelectable obj) {
        return selectedObjects.Contains(obj);
    }

    public void SelectObject(ISelectable obj) {
        if (!IsSelected(obj)) {
            selectedObjects.Add(obj);
            obj.OnSelect();
        }
    }

    public void DeselectObject(ISelectable obj) {
        if (IsSelected(obj)) {
            selectedObjects.Remove(obj);
            obj.OnDeselect();
        }
    }

    public void ClearSelection() {
        foreach (var obj in selectedObjects)
            obj.OnDeselect();
        selectedObjects.Clear();
    }

    public ISelectable[] GetSelectedObjects() {
        if (selectedObjects == null)
            return new ISelectable[0];
        return selectedObjects.ToArray();
    }

    public BaseComponent[] GetSelectedComponents() {
        if (selectedObjects == null)
            return new BaseComponent[0];
        List<BaseComponent> selectedComponents = new List<BaseComponent>();
        foreach (var obj in selectedObjects)
            if (obj is BaseComponent)
                selectedComponents.Add(obj as BaseComponent);
        return selectedComponents.ToArray();
    }

    void Awake() {
        instance = (SelectedObjects)Singleton.Setup(this, instance);
    }
}
