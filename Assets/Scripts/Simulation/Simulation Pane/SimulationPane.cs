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

    public void AddNewObject(GameObject obj) {
        obj.transform.parent = componentsContainer;
    }

    void Awake() {
        instance = (SimulationPane)Singleton.Setup(this, instance);
    }
}
