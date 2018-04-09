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
    public Transform wiresContainer;

    public List<GameObject> activeComponents = new List<GameObject>();
    public List<GameObject> activeWires = new List<GameObject>();

    public void AddNewComponent(GameObject obj) {
        obj.transform.parent = componentsContainer;
    }

    public GameObject[] GetActiveComponents() {
        return activeComponents.ToArray();
    }

    public GameObject[] GetActiveWires() {
        return activeWires.ToArray();
    }

    void Awake() {
        instance = (SimulationPane)Singleton.Setup(this, instance);
    }

    void LateUpdate() {
        activeComponents.Clear();
        foreach (Transform child in componentsContainer)
            if (child.gameObject.activeInHierarchy)
                activeComponents.Add(child.gameObject);

        activeWires.Clear();
        foreach (Transform child in wiresContainer)
            if (child.gameObject.activeInHierarchy && child.GetComponent<Wire>().wireEnabled)
                activeWires.Add(child.gameObject);
    }
}
