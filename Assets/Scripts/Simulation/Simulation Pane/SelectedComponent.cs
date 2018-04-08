using UnityEngine;

/// <summary>
/// Holds the reference to the currently selected component or wire
/// </summary>
/// Current implementation does not allow multiple selection
public class SelectedComponent : MonoBehaviour {

    static public SelectedComponent instance { get; private set; }

    /// <summary>
    /// Currently selected component or wire
    /// </summary>
    public GameObject component;

    public bool HasComponent() {
        return component != null;
    }

    public bool IsSelected(GameObject obj) {
        return component == obj;
    }

    void Awake() {
        instance = (SelectedComponent)Singleton.Setup(this, instance);
    }
}
