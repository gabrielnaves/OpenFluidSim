using UnityEngine;

public class SimulationPane : MonoBehaviour {

    static public SimulationPane instance { get; private set; }

    public void AddNewObject(GameObject obj) {
        obj.transform.parent = transform.FindChild("Components");
    }

    void Awake() {
        instance = (SimulationPane)Singleton.Setup(this, instance);
    }
}
