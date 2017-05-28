using UnityEngine;

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
