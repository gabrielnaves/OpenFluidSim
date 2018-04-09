using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simulation pane singleton instance
/// </summary>
/// Enables easy referencing to simulation pane object, and provides
/// a function for adding new components (pneumatic, etc.) to the
/// components container.
public class SimulationPane : MonoBehaviour {

    static public SimulationPane instance { get; private set; }

    public Transform componentsContainer;

    List<GameObject> activeComponents = new List<GameObject>();

    public void AddNewObject(GameObject obj) {
        obj.transform.parent = componentsContainer;
    }

    public GameObject[] GetActiveComponents() {
        return activeComponents.ToArray();
    }

    void Awake() {
        instance = (SimulationPane)Singleton.Setup(this, instance);
    }

    void LateUpdate() {
        activeComponents.Clear();
        foreach (Transform child in componentsContainer)
            if (child.gameObject.activeInHierarchy)
                activeComponents.Add(child.gameObject);
    }
}
