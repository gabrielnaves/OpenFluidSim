using UnityEngine;

public class SelectedComponent : MonoBehaviour {

    static public SelectedComponent instance { get; private set; }

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
