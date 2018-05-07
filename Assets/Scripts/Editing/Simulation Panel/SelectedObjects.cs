using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the reference to the currently selected objects
/// </summary>
public class SelectedObjects : MonoBehaviour {

    static public SelectedObjects instance { get; private set; }

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

    public List<BaseComponent> GetSelectedComponents() {
        List<BaseComponent> selectedComponents = new List<BaseComponent>();
        if (selectedObjects != null)
            foreach (var obj in selectedObjects)
                if (obj is BaseComponent)
                    selectedComponents.Add(obj as BaseComponent);
        return selectedComponents;
    }

    public List<Wire> GetSelectedWires() {
        List<Wire> selectedComponents = new List<Wire>();
        if (selectedObjects != null)
            foreach (var obj in selectedObjects)
                if (obj is Wire)
                    selectedComponents.Add(obj as Wire);
        return selectedComponents;
    }

    void Awake() {
        instance = (SelectedObjects)Singleton.Setup(this, instance);
    }
}
