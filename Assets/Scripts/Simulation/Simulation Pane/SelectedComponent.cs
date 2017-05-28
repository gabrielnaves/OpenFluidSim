using UnityEngine;

public class SelectedComponent : MonoBehaviour {

    static public SelectedComponent instance { get; private set; }

    public GameObject component { get; set; }

    void Awake() {
        instance = (SelectedComponent)Singleton.Setup(this, instance);
    }
}
