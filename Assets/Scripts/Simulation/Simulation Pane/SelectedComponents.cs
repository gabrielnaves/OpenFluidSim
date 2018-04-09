using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the reference to the currently selected components or wires
/// </summary>
public class SelectedComponents : MonoBehaviour {

    static public SelectedComponents instance { get; private set; }

    /// <summary>
    /// Currently selected components or wires
    /// </summary>
    public List<GameObject> components = new List<GameObject>();

    public bool HasComponent() {
        return components != null;
    }

    public bool HasComponentOfType<T>() {
        foreach (var obj in components)
            if (obj.GetComponent<T>() != null)
                return true;
        return false;
    }

    public T GetComponentOfType<T>() {
        foreach (var obj in components) {
            if (obj.GetComponent<T>() != null)
                return obj.GetComponent<T>();
        }
        return default(T);
    }

    public bool IsSelected(GameObject obj) {
        return components.Contains(obj);
    }

    public void SelectComponent(GameObject obj) {
        if (!IsSelected(obj))
            components.Add(obj);
    }

    public void DeselectComponent(GameObject obj) {
        if (IsSelected(obj))
            components.Remove(obj);
    }

    void Awake() {
        instance = (SelectedComponents)Singleton.Setup(this, instance);
    }

    void Update() {
        for (int i = 0; i < components.Count; ++i) {
            if (!components[i].activeInHierarchy)
                components.RemoveAt(i--);
        }
    }
}
