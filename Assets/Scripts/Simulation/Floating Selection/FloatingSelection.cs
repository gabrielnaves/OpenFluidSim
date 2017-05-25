using UnityEngine;

public class FloatingSelection : MonoBehaviour {

    static public FloatingSelection instance { get; private set; }

    private GameObject floatingComponent;

    public void AddComponent(GameObject component) {
        RemoveCurrentComponent();
        floatingComponent = component;
        component.transform.parent = transform;
    }

    public void RemoveCurrentComponent() {
        if (floatingComponent)
            DestroyImmediate(floatingComponent);
    }

	void Awake () {
        instance = (FloatingSelection)Singleton.Setup(this, instance);
	}
}
