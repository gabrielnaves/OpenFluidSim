using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simulation panel singleton instance
/// </summary>
/// Enables easy referencing to simulation panel object, and provides
/// a function for adding new components (pneumatic, etc.) to the
/// components container.
public class SimulationPanel : MonoBehaviour {

    static public SimulationPanel instance { get; private set; }

    public Transform componentsContainer;
    public Transform wiresContainer;

    public void AddNewComponent(GameObject obj) {
        obj.transform.parent = componentsContainer;
    }

    void Awake() {
        instance = (SimulationPanel)Singleton.Setup(this, instance);
    }
}
