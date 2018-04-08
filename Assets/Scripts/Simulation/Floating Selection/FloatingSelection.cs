using UnityEngine;

/// <summary>
/// The place where the current floating component stays in
/// until it is placed on editor canvas, or removed
/// </summary>
/// After the desired electric/pneumatic component is selected on the menu
/// the corresponding object is instantiated as a floating component, and
/// kept on the floating selection. When the component's position is
/// chosen the actual component game object is instantiated and added to
/// the simulation pane.
public class FloatingSelection : MonoBehaviour {

    static public FloatingSelection instance { get; private set; }

    GameObject floatingComponent;

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
